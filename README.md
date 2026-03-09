Ovaj projek je napravljen kao testni zadatak za posao.
U testnom zadatku je bilo potrebno napraviti Web API aplikaciju koja bi upravljala proizvodima i kategorijama

Aplikacija omogucuje - CRUD operacije nad proizvodima (Dohvat svih, dohvat(id), kreiranje, update, brisanje) 
				     - CRUD operacije nad kategorijama (Dohvat svih, dohvat(id), kreiranje, update, brisanje) 
					 - Povezivanje (vise) proizvoda sa kategorijama 
					 - Sortiranje/ Filtriranje/ Paginaciju kod dohvacanja proizvoda 
					 - Vracanje DTO-a a ne Entiteta

Arhitektura aplikacije - Project.Service (Entity, Interface, Service) 
					   - Project.WebAPI (DTO's, Controlleri)

Sva poslovna logika se nalazi unutar Project.Service/Services

Pokretanje aplikacije - Klonirati repozitoriji 
					  - Postaviti Connection string u appsettings.json 
					  - Pokrenuti migracije - Add-Migration 
											- Update-Database 
					  - Pokretanje aplikacije 
					  - Testiranje pomoci Swaggera/Postmana

Unutar "ZadatakZaPosao/Project.Service/Data/ApplicationDbContext.cs" je seedano nekoliko kategorija i proizvoda.


Prilikom izrade WEB API-a su koristeni sljedeci alati - Visual Studio 2022 
													  - Entity Framework Core (9.0.13) 
													  - SQL SMS 20.2 
													  - Swagger


2. Verzija
	Popravljena je greska sa seedanjem podataka prilikom migracije. 
	Jednostavan exception handling je odradjen pomoci try-catch blokova 
	Include() je proucen i dodan 
	AsNotracking() je takodjer proucen i dodan gdje je potrebno
	Async/await je pregledan te uklonjen sa odradjenih mjesta gdje nije bio potreban
