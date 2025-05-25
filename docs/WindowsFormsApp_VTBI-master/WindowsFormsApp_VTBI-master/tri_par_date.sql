select * from   TradeSet where 
--Statut='CLOSE'
--and 
 DateHeureDebut>=CONVERT(DATETIME, '2021-06-23', 102)
order by DateHeureDebut desc
--and DateHeureDebut>'23/06/2021 00:00:00'
