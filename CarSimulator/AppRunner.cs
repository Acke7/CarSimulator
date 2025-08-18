using CarSimulator.RandomUser;
using CarSimulatorCore.Dtos;
using CarSimulatorCore.Enums;
using CarSimulatorCore.Models;
using CarSimulatorCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulator
{
    public  class AppRunner
    {
        private readonly Driver _driver;
        private readonly ISimulatorService _sim;
        private readonly IRandomUserClient _randomUserClient;

        public AppRunner(Driver driver, ISimulatorService sim, IRandomUserClient randomUserClient)
        {
            _driver = driver;
            _sim = sim;
            _randomUserClient = randomUserClient;
        }

        public async Task RunAsync()
        {
            try
            {
                _driver.Rename(await _randomUserClient.GetRandomFullNameAsync());
            }
            catch
            {
                // If the API fails, keep default name
            }

            Console.Title = "Car Simulator";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                PrintMenu(_driver.Name);

                var input = Console.ReadLine();
                if (!int.TryParse(input, out var choice) || choice < 1 || choice > 7)
                {
                    WriteInfo("Invalid choice. Please enter a number 1-7.");
                    continue;
                }

                var command = (Command)choice;
                if (command == Command.Exit)
                {
                    WriteInfo("Goodbye! 👋");
                    break;
                }

                var result = _sim.Apply(command);
                PrintResult(result);
            }
        }

        private static void PrintMenu(string driverName)
        {
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"Driver: {driverName}");
            Console.WriteLine("Choose an action:");
            Console.WriteLine("  1) Turn Left");
            Console.WriteLine("  2) Turn Right");
            Console.WriteLine("  3) Drive Forward");
            Console.WriteLine("  4) Reverse");
            Console.WriteLine("  5) Rest");
            Console.WriteLine("  6) Refuel");
            Console.WriteLine("  7) Exit");
            Console.Write("Your choice: ");
        }

        private static void PrintResult(ActionResult r)
        {
            Console.WriteLine();

            WriteInfo(r.Message);

            Console.Write("Direction: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(r.Direction);
            Console.ResetColor();

            Console.Write("Fuel: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{r.Fuel}%");
            Console.ResetColor();

            Console.Write("Fatigue: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(r.Fatigue);
            Console.ResetColor();

            if (!string.IsNullOrWhiteSpace(r.WarningText))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(r.WarningText);
                Console.ResetColor();
            }
        }

        private static void WriteInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
