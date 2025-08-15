using CarSimulatorCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Models
{
    public  class Car
    {
        public Direction Direction { get; private set; }
        public int Fuel { get; private set; } // 0..100

        public Car(Direction direction = Direction.North, int fuel = 100)
        {
            Direction = direction;
            Fuel = Math.Clamp(fuel, 0, 100);
        }

        public bool HasFuel => Fuel > 0;

        public void Refuel(int to = 100) => Fuel = Math.Clamp(to, 1, 100);

        public bool Consume(int amount)
        {
            if (Fuel <= 0) return false;
            Fuel = Math.Max(0, Fuel - Math.Max(0, amount));
            return true;
        }

        public void TurnLeft() => Direction = Direction.TurnLeft();
        public void TurnRight() => Direction = Direction.TurnRight();
    }
}
