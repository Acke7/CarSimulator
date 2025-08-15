using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Enums
{
    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public static class DirectionOps
    {
        public static Direction TurnLeft(this Direction d)
            => (Direction)(((int)d + 3) % 4); // -1 mod 4
        public static Direction TurnRight(this Direction d)
            => (Direction)(((int)d + 1) % 4);
    }
}
