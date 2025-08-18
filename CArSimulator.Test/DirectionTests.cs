using Microsoft.VisualStudio.TestTools.UnitTesting;
using CArSimulator.Test.Helper;   // TestFactory & TestSetup
using CarSimulatorCore.Enums;     // Direction, Command

namespace CArSimulator.Test
{
    [TestClass]
    public class DirectionTests
    {
        [TestMethod]
        public void TurnLeft_Cycle()
        {
            var setup = TestFactory.Create(fuel: 100);
            Assert.AreEqual(Direction.North, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnLeft);
            Assert.AreEqual(Direction.West, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnLeft);
            Assert.AreEqual(Direction.South, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnLeft);
            Assert.AreEqual(Direction.East, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnLeft);
            Assert.AreEqual(Direction.North, setup.Car.Direction);
        }

        [TestMethod]
        public void TurnRight_Cycle()
        {
            var setup = TestFactory.Create(fuel: 100);

            setup.Sim.Apply(Command.TurnRight);
            Assert.AreEqual(Direction.East, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnRight);
            Assert.AreEqual(Direction.South, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnRight);
            Assert.AreEqual(Direction.West, setup.Car.Direction);

            setup.Sim.Apply(Command.TurnRight);
            Assert.AreEqual(Direction.North, setup.Car.Direction);
        }

        [TestMethod]
        public void Forward_DoesNotChangeDirection()
        {
            var setup = TestFactory.Create(fuel: 100);
            setup.Sim.Apply(Command.Forward);
            Assert.AreEqual(Direction.North, setup.Car.Direction);
        }
    }
}
