import time
from datetime import datetime, timedelta
import subprocess

SCRIPT = "main_signal_to_db_5m.py"

def wait_until_next_5m_plus_1():
    now = datetime.now()
    # Prochaine minute multiple de 5
    next_minute = (now.minute // 5 + 1) * 5
    if next_minute == 60:
        next_time = now.replace(minute=0, second=0, microsecond=0) + timedelta(hours=1)
    else:
        next_time = now.replace(minute=next_minute, second=0, microsecond=0)
    # Ajoute 1 minute de délai
    next_time = next_time + timedelta(minutes=1)
    wait_seconds = (next_time - now).total_seconds()
    print(f"Attente jusqu'à {next_time.strftime('%H:%M:%S')} ({int(wait_seconds)}s)")
    time.sleep(wait_seconds)

while True:
    wait_until_next_5m_plus_1()
    print(f"Lancement du script {SCRIPT} à {datetime.now().strftime('%H:%M:%S')}")
    subprocess.run(["python", SCRIPT])
