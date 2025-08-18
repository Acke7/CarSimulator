using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarSimulatorCore.Models;
using CarSimulatorCore.Enums;
using CarSimulatorCore.Services;
using CarSimulator.RandomUser;

namespace CarSimulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    // Core rules
                    services.AddSingleton(new FuelRules(1, 2, 100));
                    services.AddSingleton(new FatigueRules(5, 10, 2));

                    // Domain models
                    services.AddSingleton<Car>(_ => new Car(Direction.North, 100));
                    services.AddSingleton<Driver>(sp =>
                        new Driver("Loading...", sp.GetRequiredService<FatigueRules>()));

                    // Services
                    services.AddSingleton<ISimulatorService, SimulatorService>();

                    // Random User API (typed client)
               services.AddSingleton<IRandomUserClient, RandomUserClient>();

                    // App runner (your console loop)
                    services.AddSingleton<AppRunner>();
                })
                .Build();

            await host.Services.GetRequiredService<AppRunner>().RunAsync();
        }
    }
}
