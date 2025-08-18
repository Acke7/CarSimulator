using CarSimulatorCore.Dtos;
using CarSimulatorCore.Enums;
using CarSimulatorCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Services
{
    public sealed class SimulatorService : ISimulatorService
    {
        private readonly Car _car;
        private readonly Driver _driver;
        private readonly FuelRules _fuelRules;

        public SimulatorService(Car car, Driver driver, FuelRules fuelRules)
        {
            _car = car ?? throw new ArgumentNullException(nameof(car));
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _fuelRules = fuelRules ?? throw new ArgumentNullException(nameof(fuelRules));
            _fuelRules.Validate();
        }

        public ActionResult Apply(Command command)
        {
            // Exit is handled by the Console layer (menu), not here.
            return command switch
            {
                Command.TurnLeft => DoTurn(left: true),
                Command.TurnRight => DoTurn(left: false),
                Command.Forward => DoMove(forward: true),
                Command.Reverse => DoMove(forward: false),
                Command.Rest => DoRest(),
                Command.Refuel => DoRefuel(),
                _ => new ActionResult(
                        "Unknown command.",
                        _car.Direction,
                        _car.Fuel,
                        _driver.Fatigue,
                        _driver.WarningText)
            };
        }

        private ActionResult DoTurn(bool left)
        {
            if (!_car.HasFuel)
                return BlockedByFuel();

           
            _car.Consume(_fuelRules.FuelPerTurn);

            if (left) _car.TurnLeft(); else _car.TurnRight();
            _driver.IncreaseFatigue();

            var msg = left ? "Turned left." : "Turned right.";
            return Snapshot(msg);
        }

        private ActionResult DoMove(bool forward)
        {
            if (!_car.HasFuel)
                return BlockedByFuel();

           
            _car.Consume(_fuelRules.FuelPerMove);

            // Forward/Reverse do NOT change facing direction (realistic reversing).
            // If you want Reverse to flip 180°, change direction here.

            _driver.IncreaseFatigue();

            var msg = forward ? "Drove forward." : "Reversed.";
            return Snapshot(msg);
        }

        private ActionResult DoRest()
        {
            // Rest does not consume fuel
            _driver.Rest();
            return Snapshot("Driver rested.");
        }

        private ActionResult DoRefuel()
        {
            _car.Refuel(); 
            _driver.IncreaseFatigue();
            return Snapshot("Refueled to 100%.");
        }

        private ActionResult BlockedByFuel()
            => new(
                "Fuel empty. Please refuel before moving/turning.",
                _car.Direction,
                _car.Fuel,
                _driver.Fatigue,
                _driver.WarningText);

        private ActionResult Snapshot(string message)
            => new(
                message,
                _car.Direction,
                _car.Fuel,
                _driver.Fatigue,
                _driver.WarningText);
    }
}
