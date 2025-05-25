-- À exécuter en superutilisateur ou propriétaire du schéma
-- Donne tous les droits sur le schéma testresult à vt_base_user, pour les objets existants et futurs

-- Droits sur le schéma
GRANT USAGE ON SCHEMA testresult TO vt_base_user;

-- Droits sur toutes les tables existantes
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA testresult TO vt_base_user;

-- Droits sur toutes les séquences existantes
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA testresult TO vt_base_user;

-- Droits par défaut pour les futures tables
ALTER DEFAULT PRIVILEGES IN SCHEMA testresult
GRANT ALL PRIVILEGES ON TABLES TO vt_base_user;

-- Droits par défaut pour les futures séquences
ALTER DEFAULT PRIVILEGES IN SCHEMA testresult
GRANT ALL PRIVILEGES ON SEQUENCES TO vt_base_user;
