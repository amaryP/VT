select
Methode_code_methode,
sum(gain) as gain ,sum(perte) as perte,sum(gain)-sum(perte) as resultat
from TradeSet 
where Statut='CLOSE'
and
DateHeureDebut>=CONVERT(DATETIME, '2021-06-24', 102)

group by Methode_code_methode
order by Methode_code_methode
