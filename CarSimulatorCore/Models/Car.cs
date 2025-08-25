using CarSimulatorCore.Enums;
using System;

namespace CarSimulatorCore.Models
{
    public class Car
    {
        public Direction Direction { get; private set; }
        public int Fuel { get; private set; } // 0–100

        public Car(Direction direction = Direction.North, int fuel = 100)
        {
            Direction = direction;
            Fuel = Math.Clamp(fuel, 0, 100);
        }
        //If Fuel is greater than 0, it returns true.
        public bool HasFuel => Fuel > 0;

        public void Refuel()
        {

            Fuel += 100; 
            if (Fuel == 100|| Fuel >100)
            {
                Fuel = 100;
            }
        }

        public bool Consume(int amount)
        {
            if (!HasFuel) return false;

            Fuel -= amount;
            if (Fuel < 0) Fuel = 0;

            return true;
        }

        public void TurnLeft()
        {
            Direction = Direction.TurnLeft();
        }

        public void TurnRight()
        {
            Direction = Direction.TurnRight();
        }
    }
}
