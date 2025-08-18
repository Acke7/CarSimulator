using CarSimulatorCore.Enums;
using CarSimulatorCore.Models;
using CarSimulatorCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CArSimulator.Test.Helper
{
 

    public static class TestFactory
    {
        public static TestSetup Create(
            int fuel = 100, int fatigue = 0,
            int warn = 5, int max = 10, int restRecovery = 2,
            int fuelPerTurn = 1, int fuelPerMove = 2)
        {
            var car = new Car(Direction.North, fuel);
            var driver = new Driver("Test Driver", new FatigueRules(warn, max, restRecovery), fatigue);
            var sim = new SimulatorService(car, driver, new FuelRules(fuelPerTurn, fuelPerMove, 100));
            return new TestSetup(sim, car, driver);
        }
    }
   
    }
