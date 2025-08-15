using CarSimulatorCore.Dtos;
using CarSimulatorCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Services
{
    public interface ISimulatorService
    {
        ActionResult Apply(Command command);
    }
}
