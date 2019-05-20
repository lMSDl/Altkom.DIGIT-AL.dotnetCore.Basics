using System;
using Microsoft.Extensions.Configuration;
using Altkom.DIGIT_AL.dotnetCore.Basics.Program.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.Program.Services;
using Microsoft.Extensions.DependencyInjection;
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
            foreach(var service in ServiceProvider.GetServices<IConsoleWriteLineService>()) {
                service.Execute("Hello");
            }
        }
    }
}
