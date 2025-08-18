using System;

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
        {
            // Turning left is the same as subtracting 1 (or +3 mod 4)
            return (Direction)(((int)d + 3) % 4);
        }

        public static Direction TurnRight(this Direction d)
        {
            // Turning right is the same as adding 1 (mod 4)
            return (Direction)(((int)d + 1) % 4);
        }
    }
}
