-- Table pour stocker tous les signaux bruts récupérés via l'API taapi.io
-- Permet d'analyser a posteriori tous les signaux, même ceux qui ne déclenchent pas d'événement

CREATE TABLE IF NOT EXISTS signaux_bruts (
    id SERIAL PRIMARY KEY,
    symbol VARCHAR(20) NOT NULL,
    dateheure TIMESTAMP NOT NULL,
    rsi14 NUMERIC(6,2),
    rsi5 NUMERIC(6,2),
    bb_upper NUMERIC(18,5),
    bb_lower NUMERIC(18,5),
    bb_mid NUMERIC(18,5),
    ema NUMERIC(18,5),
    close NUMERIC(18,5),
    open NUMERIC(18,5),
    high NUMERIC(18,5),
    low NUMERIC(18,5),
    pattern VARCHAR(50),
    eventlog TEXT,
    raw_json JSONB,
    created_at TIMESTAMP DEFAULT now(),
    valeur NUMERIC(18,5)
);
