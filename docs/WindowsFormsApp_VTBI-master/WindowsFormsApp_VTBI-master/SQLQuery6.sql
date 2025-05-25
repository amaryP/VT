select
CodeMethodeTriggerTrade,
CodeMethodeSuivi,
sum(gain) as gain ,sum(perte) as perte,sum(gain)-sum(perte) as resultat
from TradeSet 
where Statut='CLOSE'
and
DateHeureDebut>=CONVERT(DATETIME, '2021-06-26', 102)
group by CodeMethodeTriggerTrade,CodeMethodeSuivi
order by resultat desc
--order by CodeMethodeTriggerTrade,CodeMethodeSuivi
