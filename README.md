# Monitorování stromů
* Prezentovat stav (dosavadní implementaci) a zamýšlený progres (další plánované vlastnosti) do 10. 11. 2024 (na cvičeních, případně dle stanovených požadavků vašeho cvičícího)
  * Při nesplnění prezentace stavu (nic neimplementováno/neprezentováno) je nesplněna podmínka pro získání zápočtu v 1. pokusu
  * Při nedostatečném rozsahu zamýšlené práce budou cvičícím navrženy nutné vlastnosti, které je třeba dále realizovat.
* Odevzdat a obhájit sem. práci (na cvičení, případně dle stanovených požadavků vašeho cvičícího) do 
  * Pátek 20. 12. 2024 23:59 – BCSH2

## Idea
* Sledované entity: strom - správce - region
* Každý správce, monitoruje nějaké stromy a každý správce spadá pod nějaký region.
* Aplikace bude umožňovat vytvářet, editovat a mazat veškeré typy entit.
* Aplikace bude také umožňovat vyhledávání pomocí lokace, správce či rozpětí data zasazení stromu.
* Pro perzistenci dat je využito knihovny pro [SQLite](https://www.nuget.org/packages/Microsoft.Data.Sqlite.Core/), která je závislá na [SQLitePCLRaw.core](https://www.nuget.org/packages/SQLitePCLRaw.core)
