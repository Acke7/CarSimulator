using Microsoft.VisualStudio.TestTools.UnitTesting;
using CArSimulator.Test.Helper;   // matches your TestFactory/TestSetup
using CarSimulatorCore.Enums;

namespace CArSimulator.Test           // match namespace (note the capital 'A' in CAr)
{
    [TestClass]
    public class FatigueTests_LegacyStyle // rename to avoid class-name collision
    {
        [TestMethod]
        public void Increases_OnMove()
        {
            var setup = TestFactory.Create(fatigue: 0);
            setup.Sim.Apply(Command.Forward);
            Assert.AreEqual(1, setup.Driver.Fatigue);
        }

        [TestMethod]
        public void Increases_OnTurn()
        {
            var setup = TestFactory.Create(fatigue: 0);
            setup.Sim.Apply(Command.TurnRight);
            Assert.AreEqual(1, setup.Driver.Fatigue);
        }

        [TestMethod]
        public void Increases_OnRefuel()
        {
            var setup = TestFactory.Create(fatigue: 0);
            setup.Sim.Apply(Command.Refuel);
            Assert.AreEqual(1, setup.Driver.Fatigue);
        }

        [TestMethod]
        public void Rest_Reduces_NotBelowZero()
        {
            var setup = TestFactory.Create(fatigue: 1, restRecovery: 5);
            setup.Sim.Apply(Command.Rest);
            Assert.AreEqual(0, setup.Driver.Fatigue);
        }

        [TestMethod]
        public void Warning_AtThreshold()
        {
            var setup = TestFactory.Create(fatigue: 4, warn: 5, max: 10);
            setup.Sim.Apply(Command.Refuel); // +1 => 5
            Assert.IsTrue(setup.Driver.IsWarning);
            Assert.IsFalse(setup.Driver.IsMax);
        }

        [TestMethod]
        public void Max_AtMax()
        {
            var setup = TestFactory.Create(fatigue: 9, warn: 5, max: 10);
            setup.Sim.Apply(Command.Refuel); // +1 => 10
            Assert.IsTrue(setup.Driver.IsMax);
        }
    }
}
