-- À exécuter en superutilisateur ou propriétaire du schéma
-- Donne tous les droits sur toutes les tables et séquences de tous les schémas à vt_base_user

DO $$
DECLARE
    r RECORD;
BEGIN
    -- Pour chaque schéma utilisateur (hors systeme)
    FOR r IN SELECT nspname FROM pg_namespace WHERE nspname NOT LIKE 'pg_%' AND nspname <> 'information_schema' LOOP
        EXECUTE format('GRANT USAGE ON SCHEMA %I TO vt_base_user;', r.nspname);
        EXECUTE format('GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA %I TO vt_base_user;', r.nspname);
        EXECUTE format('GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA %I TO vt_base_user;', r.nspname);
        EXECUTE format('ALTER DEFAULT PRIVILEGES IN SCHEMA %I GRANT ALL PRIVILEGES ON TABLES TO vt_base_user;', r.nspname);
        EXECUTE format('ALTER DEFAULT PRIVILEGES IN SCHEMA %I GRANT ALL PRIVILEGES ON SEQUENCES TO vt_base_user;', r.nspname);
    END LOOP;
END $$;
