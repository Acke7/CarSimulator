using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Models
{

    public sealed class FuelRules
    {
        public int FuelPerTurn { get; }
        public int FuelPerMove { get; }
        public int RefuelTo { get; }

        public FuelRules(int fuelPerTurn = 1, int fuelPerMove = 2, int refuelTo = 100)
        {
            FuelPerTurn = fuelPerTurn;
            FuelPerMove = fuelPerMove;
            RefuelTo = refuelTo;
        }

        public void Validate()
        {
            if (FuelPerTurn < 0 || FuelPerMove < 0 || RefuelTo <= 0)
                throw new ArgumentException("Invalid fuel rules.");
        }
    }
}
