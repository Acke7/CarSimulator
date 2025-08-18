using Microsoft.VisualStudio.TestTools.UnitTesting;
using CArSimulator.Test.Helper;   // TestFactory & TestSetup
using CarSimulatorCore.Enums;     // Command

namespace CArSimulator.Test
{
    [TestClass]
    public class SimulatorServiceTests
    {
        [TestMethod]
        public void Snapshot_ContainsStatusAndWarning()
        {
            var setup = TestFactory.Create(fatigue: 4, warn: 5, max: 6);

            var r1 = setup.Sim.Apply(Command.Forward); // fatigue 5 -> warning
            Assert.AreEqual("Drove forward.", r1.Message);
            Assert.IsNotNull(r1.WarningText);
            Assert.AreEqual(setup.Driver.Fatigue, r1.Fatigue);

            var r2 = setup.Sim.Apply(Command.Refuel); // fatigue 6 -> max
            Assert.IsTrue(r2.WarningText!.Contains("MAX fatigue"));
        }

        [TestMethod]
        public void BlockedByFuel_Message()
        {
            var setup = TestFactory.Create(fuel: 0);
            var r = setup.Sim.Apply(Command.TurnLeft);
            Assert.AreEqual(0, setup.Car.Fuel);
            StringAssert.Contains(r.Message, "Fuel empty");
        }

        [TestMethod]
        public void Rest_DoesNotConsumeFuel()
        {
            var setup = TestFactory.Create(fuel: 10, fatigue: 5, restRecovery: 2);
            var before = setup.Car.Fuel;

            setup.Sim.Apply(Command.Rest);

            Assert.AreEqual(before, setup.Car.Fuel);
            Assert.AreEqual(3, setup.Driver.Fatigue);
        }

        [TestMethod]
        public void Refuel_AlwaysTo100()
        {
            var setup = TestFactory.Create(fuel: 55);
            var r = setup.Sim.Apply(Command.Refuel);

            Assert.AreEqual(100, setup.Car.Fuel);
            Assert.AreEqual(1, setup.Driver.Fatigue);
            StringAssert.Contains(r.Message, "Refueled");
        }
    }
}
