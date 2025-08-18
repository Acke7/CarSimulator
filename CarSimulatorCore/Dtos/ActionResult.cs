using CarSimulatorCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Dtos
{
    public class ActionResult
    {
        public string Message { get; }
        public Direction Direction { get; }
        public int Fuel { get; }
        public int Fatigue { get; }
        public string? WarningText { get; }

        public ActionResult(string message, Direction direction, int fuel, int fatigue, string? warningText = null)
        {
            Message = message;
            Direction = direction;
            Fuel = fuel;
            Fatigue = fatigue;
            WarningText = warningText;
        }
    }
}
