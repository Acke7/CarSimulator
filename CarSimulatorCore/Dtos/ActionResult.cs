using CarSimulatorCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Dtos
{
    public class  ActionResult(
      string Message,
      Direction Direction,
      int Fuel,
      int Fatigue,
      string? WarningText = null
  );
}
