using System;
using Microsoft.Extensions.Configuration;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("configapp.json", optional: true, reloadOnChange: true)
            .AddIniFile("configapp.ini", optional: true, reloadOnChange: true)
            .AddXmlFile("configapp.xml", optional: true, reloadOnChange: true)
            .AddYamlFile("configapp.yaml", optional: true, reloadOnChange: true)
            .Build();

            Console.WriteLine(
                Figgle.FiggleFonts.Standard.Render($"Hello {config["HelloYaml"]}!"));
        }
    }
}
