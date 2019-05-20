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
        }

        static void Main(string[] args)
        {
            var logger = ServiceProvider.GetService<ILogger<Program>>();
                logger.LogTrace("Enter Main");

                foreach(var service in ServiceProvider.GetServices<IConsoleWriteLineService>()) {
                    
            using(logger.BeginScope($"Wyświetlanie wiadomości z serwisu {service.GetType().Name}")) {
                    logger.LogDebug($"Service says \"Hello\"");
                    service.Execute("Hello");
                    logger.LogDebug($"Service said \"Hello\"");
            }
                }

            logger.LogTrace("Exit Main");
            Console.ReadKey();
        }
    }
}
