using CarSimulatorCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulator.NunitTest
{
   [TestFixture]
    public class DirectionCaseTests
    {
        [TestCase(Direction.North, Direction.West)]
        [TestCase(Direction.West, Direction.South)]
        [TestCase(Direction.South, Direction.East)]
        [TestCase(Direction.East, Direction.North)]
        public void TurnLeft_RotatesCorrectly(Direction start, Direction expected)
        {
            var result = start.TurnLeft();
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
