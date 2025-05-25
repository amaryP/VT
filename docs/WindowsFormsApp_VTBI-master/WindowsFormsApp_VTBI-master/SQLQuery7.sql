select symbol,CodeMethodeTriggerTrade,CodeMethodeSuivi,DateHeureDebut,DateHeureFin, ValeurAchat,ValeurVente,ValeurCourante,STOP_COURANT,quantiteachat,gain,perte from dbo.TradeSet
where 
ValeurAchat>ValeurCourante and 
Statut='OPEN'
--and

--CodeMethodeTriggerTrade='NEW4'
---and CodeMethodeSuivi='STANDARD'
order by DateHeureDebut