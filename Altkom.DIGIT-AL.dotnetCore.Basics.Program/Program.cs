using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Altkom.DIGIT_AL.dotnetCore.Basics.Program.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.Program.Services;
using StructureMap;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using Altkom.DIGIT_AL.dotnetCore.Basics.Services;
using Newtonsoft.Json;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using System.Net.Http.Headers;

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

        static async Task Main(string[] args)
        {
            //ConsoleTest();
            //await TaskTests();

            using(var httpClient = new HttpClient()) {
            httpClient.BaseAddress = new Uri("http://localhost:5000");

            var user = new User{Username = "test1", Password = "test2"};
            user.Token = await new AuthService(httpClient).Authenticate(user);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
            //httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {user.Token}");

            var customersService = new CustomersService(httpClient);
            var customers = await customersService.GetAsync();
            Console.WriteLine(JsonConvert.SerializeObject(customers));
            
            var customer = await customersService.GetAsync(2);
            Console.WriteLine(JsonConvert.SerializeObject(customer));

            customer.FirstName = customer.LastName;
            customer = await customersService.AddAsync(customer);
            Console.WriteLine(JsonConvert.SerializeObject(customer));

            customer.FirstName = "Adam";
            var result = await customersService.UpdateAsync(customer);
            Console.WriteLine(result);
            customer = await customersService.GetAsync(customer.Id);
            Console.WriteLine(JsonConvert.SerializeObject(customer));

            result = await customersService.DeleteAsync(customer.Id);
            Console.WriteLine(result);

            customers = await customersService.GetAsync();
            Console.WriteLine(JsonConvert.SerializeObject(customers));

            }
                        
            Console.ReadKey();
        }

        private static async Task TaskTests()
        {
            //await Task.WhenAll(new Task[] {CounterAsync(10), CounterAsync(5)});

            foreach (var i in new[] { 1, 2, 3, 4 }.ToList())
                await CounterAsync(i);

            new[] {1, 2}.ToList().ForEach(async x => await CounterAsync(x));

            new[] {3, 4}.ToList().AsParallel().ForAll(async x => await CounterAsync(x));
            // Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Starting 1+1");
            // var a = Sum(1, 1).Result;

            // Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Starting 2+2");
            // var b = await Sum(1, 1);

            // Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Starting 5");
            //     CounterAsync(5).Wait();
            // Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Starting 7");
            //     await CounterAsync(7);
            // Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Starting 10");
            //     _ = CounterAsync(10);
            Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Stopping");
        }

        static async Task CounterAsync(int amount) {
            Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Start counting {amount}");
            await Task.Delay(amount*1000);
            Logger.LogDebug($"{Thread.CurrentThread.ManagedThreadId} - Stop counting {amount}");
        }

        static Task<int> Sum(int a, int b) {
            return Task.FromResult(a+b);
        }


        private static void ConsoleTest()
        {
            Logger.LogTrace("Enter Main");

            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;


            foreach (var service in ServiceProvider.GetServices<IConsoleWriteLineService>())
            {
                using (Logger.BeginScope($"Wyświetlanie wiadomości z serwisu {service.GetType().Name}"))
                {
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
