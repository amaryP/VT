delete 
--sum(gain),sum(perte),sum(gain)-sum(perte)
from TradeSet where Statut='CLOSE'
--and
--DateHeureDebut>=CONVERT(DATETIME, '2021-06-23', 102)
