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
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            //.AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            //.AddXmlFile("configapp.xml", optional: true, reloadOnChange: true)
            //.AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .Build();

            var settings = new Settings();
            config.Bind(settings);
            var section = new Section();
            config.GetSection("Section").Bind(section);

            //var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection
            .AddScoped<IConsoleWriteLineService, ConsoleWriteLineService>()
            .AddScoped<IConsoleWriteLineService, ConsoleWriteFiggleLineService>()
            .BuildServiceProvider();

            

            foreach(var service in serviceProvider.GetServices<IConsoleWriteLineService>()) {
                service.Execute("Hello");
            }
        }
    }
}
