using CarSimulatorCore.Models;
using CarSimulatorCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CArSimulator.Test.Helper
{
    public class TestSetup
    {
        public SimulatorService Sim { get; }
        public Car Car { get; }
        public Driver Driver { get; }

        public TestSetup(SimulatorService sim, Car car, Driver driver)
        {
            Sim = sim;
            Car = car;
            Driver = driver;
        }
    }
}
