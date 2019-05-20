using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Altkom.DIGIT_AL.dotnetCore.Basics.Program.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.Program.Services;
using StructureMap;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Program
{
    class Program
    {
        static IConfiguration Config {get;} = new ConfigurationBuilder()
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            //.AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            //.AddXmlFile("configapp.xml", optional: true, reloadOnChange: true)
            //.AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .Build();

        static Settings Settings {get;} = new Settings();
        static Section Section {get;} = new Section();
        static IServiceProvider ServiceProvider {get;}

        static ILogger Logger {get;}
        static Program() {
            Config.Bind(Settings);
            Config.GetSection("Section").Bind(Section);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => 
                builder.AddConsole()
                //builder.AddConsole(x => x.IncludeScopes = false)
                .AddDebug()
                .AddConfiguration(Config.GetSection("Logging")));
                //.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug);

            /*var serviceProvider = serviceCollection
            .AddScoped<IConsoleWriteLineService, ConsoleWriteLineService>()
            .AddScoped<IConsoleWriteLineService, ConsoleWriteFiggleLineService>()
            .BuildServiceProvider();*/

            var container = new Container();
            container.Configure(configurationExpression => {
                configurationExpression.Scan(x => {
                    x.AssemblyContainingType(typeof(Program));
                    x.AddAllTypesOf<IConsoleWriteLineService>();
                    x.WithDefaultConventions();
                });
                configurationExpression.Populate(serviceCollection);
            });
            ServiceProvider = container.GetInstance<IServiceProvider>();
            
            Logger = ServiceProvider.GetService<ILogger<Program>>();
                
        }

        static void Main(string[] args)
        {
            Logger.LogTrace("Enter Main");

            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;


            foreach(var service in ServiceProvider.GetServices<IConsoleWriteLineService>()) {
                using(Logger.BeginScope($"Wyświetlanie wiadomości z serwisu {service.GetType().Name}")) {
                        Logger.LogDebug($"Service says \"Hello\"");
                        service.Execute("Hello");
                        // if(service.GetType().Name == nameof(ConsoleWriteFiggleLineService)) {
                        //     throw new Exception($"{service.GetType().Name} failed");
                        // }
                        Logger.LogDebug($"Service said \"Hello\"");
                }
            }

            Logger.LogTrace("Exit Main");
            Console.ReadKey();
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args) {
            Logger.LogError(args.ExceptionObject.ToString());
            
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
