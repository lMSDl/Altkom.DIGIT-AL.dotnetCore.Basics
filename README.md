# Podstawy .NET Core 2.2

## CLI
* Nowy projekt
  * konsolowy
  ```
  dotnet new console [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>]
  ```
  * WebAPI
  ```
  dotnet new webapi [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>] [--no-https]
  ``` 
  * Razor Pages
  ```
  dotnet new webapp [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>] [--no-https]
  ``` 
  * MVC
  ```
  dotnet new mvc [-o <LOKALIZACJA> -n <NAZWA_PROEKTU>] [--no-https]
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
dotnet [watch] run [PARAMETRY]
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

* Silne typowanie
```
dotnet add package Microsoft.Extensions.Configuration.Binder
```
``` c#
var settings = new Settings();
config.Bind(settings);
```

* Pliki
  * [appsettings.json](Core.Basics.Program/appsettings.json)
  * [appsettings.xml](Core.Basics.Program/appsettings.xml)
  * [appsettings.ini](Core.Basics.Program/appsettings.ini)
  * [appsettings.yaml](Core.Basics.Program/appsettings.yaml)
  * [Settings.cs](Core.Basics.Program/Models/Settings.cs)

## Wstrzykiwanie zależności
```
dotnet add package Microsoft.Extensions.DependencyInjection
```
``` c#
var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection
      .AddScoped<IService, Service>()
      .BuildServiceProvider();
var services = serviceProvider.GetServices<IService>();
```
* StructureMap
```
dotnet add package Microsoft.Extensions.DependencyInjection
```
``` c#
var serviceCollection = new ServiceCollection();
var container = new Container();
container.Configure(configurationExpression => {
   configurationExpression.Scan(x => {
      x.AssemblyContainingType(typeof(Program));
      x.WithDefaultConventions();
      x.AddAllTypesOf<IService>();
   });
   configurationExpression.Populate(serviceCollection);
});
var serviceProvider = container.GetInstance<IServiceProvider>();
var services = serviceProvider.GetServices<IService>();
```

## Logowanie
```
dotnet add package Microsoft.Extensions.Logging
dotnet add package Microsoft.Extensions.Logging.Configuration
dotnet add package Microsoft.Extensions.Logging.Console
dotnet add package Microsoft.Extensions.Logging.Debug
```
``` c#
var serviceCollection = new ServiceCollection()
   .AddLogging(builder => builder
      .AddConsole()
      .AddDebug()
      .AddConfiguration(Config.GetSection("Logging")))
   .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug);
var logger = ServiceProvider.GetService<ILogger<Program>>();
logger.LogDebug("Hello");
```
* Plik konfiguracyjny
[appsettings.json](Core.Basics.Program/appsettings.json)

## ASP.NET

* Opcje serializacji Json
``` c#
services.AddMvc()
   .AddJsonOptions(options => {
      options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
      options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
      options.SerializerSettings.Converters.Add(new StringEnumConverter(camelCaseText: true));
});
```

* Obsługa XML
``` c#
services.AddMvc().AddXmlSerializerFormatters();
```
``` c#
[Produces("application/xml")]
[ApiController]
```

* Obsługa błędów
``` c#
public class ErrorDetails
{
   public int StatusCode { get; set; }
   public string Message { get; set; }
}
```
``` c#
app.UseExceptionHandler(appError =>
{
   appError.Run(async context =>
   {
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      context.Response.ContentType = "application/json";

      var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
      if(contextFeature != null)
      {
         await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails()
         {
            StatusCode = context.Response.StatusCode,
            Message = contextFeature.Error.Message
         }));
      }
   });
});
```
