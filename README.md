# Podstawy .NET Core 2.2

## CLI
* Nowy projekt
  * konsolowy
  ```
  dotnet new console [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>]
  ```
  * WebAPI
  ```
  dotnet new webapi [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>] [--no-hhtps]
  ```
  * biblioteki
  ```
  dotnet new classlib [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>]
  ```

* Kompilacja i uruchomienie
```
dotnet build
dotnet <NAZWA_PROJEKTU>.dll [<PARAMETRY>]
```
```
dotnet run [PARAMETRY]
```

* Publikacja
  * Framework-dependent deployments (FDD)
  ```
  dotnet publish -c Release
  ```
  * Self-contained deployments (SCD)
  ```
  dotnet publish -c Release -r <IDENTYFIKATOR_ŚRODOWISKA_URUCHOMIENIOWEGO>
  ```
  * Framework-dependent executables (FDE)
  ```
  dotnet publish -c Release -r <IDENTYFIKATOR_ŚRODOWISKA_URUCHOMIENIOWEGO> --self-contained false
  ```

* Pakiety i referencje
  * Dodawanie pakietów
  ```
  dotnet add package <NAZWA_PAKIETU>
  ```
  * Pobranie pakietów
  ```
  dotnet restore
  ```
  * Dodawanie referencji
  ```
  dotnet add reference <ŚCIEŻKA_PROJEKTU>
  ```

## Pobieranie konfiguracji z pliku
```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.FileExtensions
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.Xml
dotnet add package Microsoft.Extensions.Configuration.Ini
dotnet add package NetEscapades.Configuration.Yaml
```
``` c#
var config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
      .AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true)
      .AddIniFile("appsettings.ini", optional: true, reloadOnChange: true)
      .AddYamlFile("appsettings.yaml", optional: true, reloadOnChange: true)
      .Build();
var value = config["<KLUCZ>"];
```
