Instrukcja:

Kroki oznaczone (dev) możemy pominąć, jeżeli chcemy jedynie uruchomić aplikację.

Backend:
1. Instalacja niezbędnych SDK:
	- (dev) ASP.NET SDK 2.2.110: https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-2.2.110-windows-x86-installer
	- ASP.NET Core Hosting Bundle 2.2.8: https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-2.2.8-windows-hosting-bundle-installer
	
2. Przygotowanie plików systemowych:
	- Przy pomocy narzędzia do edycji tekstu uruchomionego w trybie administratora uruchamiamy plik "hosts" znajdujący się w katalogu "%SystemRoot%\system32\drivers\etc"
	- Na końcu pliku dodajemy nową linię zawierającą: "127.0.0.1 fitkidcatering.com"
	
3. Przygotowanie IIS:
	- Upewniamy się, że na naszym komputerze uruchomiony jest IIS
	- Uruchamiamy IIS Manager (wpisujemy: "inetmgr" w oknie Uruchom (Ctrl + R))
	- Dodajemy pulę aplikacji (PPM na "Pule aplikacji" -> "Dodaj pulę aplikacji..."):
		- Podajemy nazwę puli (np. FitKidCatering)
		- Wybieramy wersję środowiska .NET CLR jako "Bez kodu zarządzanego"
		- Wybieramy zarządzany tryb potokowy jako "Zarządzany"
	- Upewniamy się, że nowo utworzona pula aplikacji jest uruchomiona
	- Wchodzimy w Ustawienia zaawansowane nowo utworzonej puli (PPM na puli aplikacji -> "Ustawienia zaawansowane...")
	- Ustawiamy "Włącz aplikacje 32-bitowe" na "True"
	- Dodajemy aplikację (PPM na "Witryny" -> "Dodaj witrynę sieci Web"):
		- Podajemy nazwę witryny jako "fitkidcatering.com"
		- Wybieramy pulę aplikacji jako utworzoną we wcześniejszym kroku
		- Wybieramy ścieżkę fizyczną jako główny katalog naszego projektu
		- Podajemy nazwę hosta jako "http://fitkidcatering.com/"
		- Upewniamy się, że podany port to "80", oraz wybrany tryb to "http"
	- Upewniamy się, że nowo utworzona witryna jest uruchomiona
	
4. Przygotowanie bazy danych:
	- Instalujemy SQL Server 2017
	- W SQL Server Menagment Studio:
		- W zakładce "Databases" dodajemy nową bazę danych
			- Podajemy nazwę (np. "FitKidCatering")
		- W zakładce "Security"->"Logins" dodajemy nowy login
			- Podajemy nazwę (np. "FitKid")
			- Zmieniamy sposób autentykacji na "SQL Server authentication"
			- Podajemy hasło (np. "FitKid")
			- Odznaczamy opcje wymuszenia zmiany hasła, jego wygaśnięcia oraz wymuszenia zasad dla hasła
			- Przechodzimy do zakładki "User Mapping"
			- Zaznaczamy "Map" dla wcześniej utworzonej bazy danych
			- W dolnym panelu zaznaczamy "db_owner"
