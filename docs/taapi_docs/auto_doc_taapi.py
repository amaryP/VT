import time
import requests
from bs4 import BeautifulSoup
import os
import re
from urllib.parse import urljoin, urlparse

# --- Configuration ---
BASE_DOMAIN = "taapi.io"
BASE_URLS = [
    "https://taapi.io/documentation/",
    "https://taapi.io/indicators/"
]
SUBFOLDERS = {
    "documentation": "documentation",
    "indicators": "indicators"
}
VISITED = set()
LINKS = []

# --- Initialisation des dossiers principaux ---
for folder in SUBFOLDERS.values():
    os.makedirs(folder, exist_ok=True)

# --- Fonctions Utilitaires ---

def sanitize_filename(url):
    parsed = urlparse(url)
    path = parsed.path.strip("/")
    filename = path.replace("/", "_")
    filename = re.sub(r'[<>:"/\\|?*,]', '_', filename)
    return filename[:150] or "document"

def fetch_page(url):
    try:
        response = requests.get(url, timeout=10)
        response.raise_for_status()
        return response.text
    except Exception as e:
        print(f"[!] Erreur sur {url} : {e}")
        return None


TRANSLATE_ENDPOINTS = [
    "https://translate.argosopentech.com/translate",
    "https://libretranslate.de/translate",
    "https://libretranslate.com/translate"
]

def translate_text(text, target_lang="fr"):
    payload = {
        "q": text,
        "source": "en",
        "target": target_lang,
        "format": "text"
    }

    for endpoint in TRANSLATE_ENDPOINTS:
        try:
            time.sleep(1.5)  # Respect du rate limit
            response = requests.post(endpoint, data=payload, timeout=10)

            if response.status_code == 200:
                return response.json().get("translatedText", text)
            else:
                print(f"[!] Erreur HTTP {response.status_code} via {endpoint}")

        except requests.exceptions.RequestException as e:
            print(f"[!] Échec connexion à {endpoint} : {e}")

    print(f"[⚠️] Traduction échouée. Texte original conservé : {text}")
    return text

# --- Fonction principale : Scraping & Génération des .md ---

def parse_and_save(url):
    html = fetch_page(url)
    if not html:
        return

    soup = BeautifulSoup(html, 'html.parser')
    title = soup.title.string.strip() if soup.title else url
    md = f"# {title}\n\n"

    for tag in soup.find_all(['h1', 'h2', 'h3', 'p', 'ul', 'ol', 'pre', 'code']):
        if tag.name in ['h1', 'h2', 'h3']:
            md += f"\n## {tag.get_text(strip=True)}\n"
        elif tag.name == 'p':
            md += f"{tag.get_text(strip=True)}\n\n"
        elif tag.name in ['ul', 'ol']:
            for li in tag.find_all('li'):
                md += f"- {li.get_text(strip=True)}\n"
            md += "\n"
        elif tag.name == 'pre':
            md += f"\n```\n{tag.get_text()}\n```\n"
        elif tag.name == 'code' and tag.parent.name != 'pre':
            md += f"`{tag.get_text()}` "

    filename = sanitize_filename(url) + ".md"
    subfolder = "documentation" if "/documentation/" in url else "indicators" if "/indicators/" in url else "."
    filepath = os.path.join(subfolder, filename)

    with open(filepath, "w", encoding="utf-8") as f:
        f.write(md)

    LINKS.append((os.path.join(subfolder, filename), title))
    print(f"[✓] {url} → {filepath}")

    for a in soup.find_all('a', href=True):
        href = a['href']
        full_url = urljoin(url, href)
        if BASE_DOMAIN in full_url and full_url.startswith("https://taapi.io/") and full_url not in VISITED:
            VISITED.add(full_url)
            parse_and_save(full_url)

def crawl_and_generate_md():
    for url in BASE_URLS:
        VISITED.add(url)
        parse_and_save(url)

def write_index():
    with open("index.md", "w", encoding="utf-8") as f:
        f.write("# 📚 Index global de la documentation Taapi.io\n\n")
        for filepath, title in sorted(LINKS):
            f.write(f"- [{title}]({filepath})\n")
    print("[✓] index.md global généré.")

def generate_local_indexes():
    for folder in SUBFOLDERS.values():
        index_path = os.path.join(folder, "index.md")
        entries = []
        for fname in sorted(os.listdir(folder)):
            if fname.endswith(".md") and fname != "index.md":
                title = fname.replace(".md", "").replace("_", " ").title()
                entries.append(f"- [{title}]({fname})")
        with open(index_path, "w", encoding="utf-8") as f:
            f.write(f"# 📄 Index : {folder}\n\n" + "\n".join(entries))
        print(f"[✓] {index_path} généré.")

def generate_translations():
    for folder in SUBFOLDERS.values():
        fr_folder = os.path.join(folder, "fr")
        os.makedirs(fr_folder, exist_ok=True)

        for fname in os.listdir(folder):
            if not fname.endswith(".md") or fname == "index.md":
                continue

            src_path = os.path.join(folder, fname)
            dst_path = os.path.join(fr_folder, fname)

            with open(src_path, "r", encoding="utf-8") as f:
                lines = f.readlines()

            translated_lines = []
            in_code_block = False

            for line in lines:
                stripped = line.strip()

                if stripped.startswith("```"):
                    in_code_block = not in_code_block
                    translated_lines.append(line)
                    continue

                if in_code_block or stripped == "":
                    translated_lines.append(line)
                    continue

                if stripped.startswith("#") or stripped.startswith("- "):
                    prefix = line[:line.find(stripped)]
                    translated = translate_text(stripped)
                    translated_lines.append(f"{prefix}{translated}\n")
                else:
                    translated = translate_text(stripped)
                    translated_lines.append(translated + "\n")

            with open(dst_path, "w", encoding="utf-8") as f:
                f.writelines(translated_lines)

            print(f"[✓] Fichier traduit → {dst_path}")

# --- Exécution manuelle (décommente si besoin) ---
if __name__ == "__main__":
    # crawl_and_generate_md()
    # write_index()
    # generate_local_indexes()
    # generate_translations()
    pass
