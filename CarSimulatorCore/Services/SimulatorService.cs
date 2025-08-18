using CarSimulatorCore.Dtos;
using CarSimulatorCore.Enums;
using CarSimulatorCore.Models;
using System;

namespace CarSimulatorCore.Services
{
    public sealed class SimulatorService : ISimulatorService
    {
        private readonly Car _car;
        private readonly Driver _driver;
        private readonly FuelRules _rules;

        public SimulatorService(Car car, Driver driver, FuelRules rules)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));
            if (driver == null) throw new ArgumentNullException(nameof(driver));
            if (rules == null) throw new ArgumentNullException(nameof(rules));

            _car = car;
            _driver = driver;
            _rules = rules;
            _rules.Validate();
        }

        public ActionResult Apply(Command command)
        {
            // Exit is handled in the Console layer (menu).
            switch (command)
            {
                case Command.TurnLeft:
                    return DoTurn(true);

                case Command.TurnRight:
                    return DoTurn(false);

                case Command.Forward:
                    return DoMove(true);

                case Command.Reverse:
                    return DoMove(false);

                case Command.Rest:
                    return DoRest();

                case Command.Refuel:
                    return DoRefuel();

                default:
                    return Result("Unknown command.");
            }
        }

        private ActionResult DoTurn(bool left)
        {
            if (!_car.HasFuel)
            {
                return Result("Fuel empty. Please refuel before moving/turning.");
            }

            _car.Consume(_rules.FuelPerTurn);

            if (left)
            {
                _car.TurnLeft();
            }
            else
            {
                _car.TurnRight();
            }

            _driver.IncreaseFatigue();

            string message = left ? "Turned left." : "Turned right.";
            return Result(message);
        }

        private ActionResult DoMove(bool forward)
        {
            if (!_car.HasFuel)
            {
                return Result("Fuel empty. Please refuel before moving/turning.");
            }

            _car.Consume(_rules.FuelPerMove);

            // Forward/Reverse do NOT change facing direction (realistic reversing).
            _driver.IncreaseFatigue();

            string message = forward ? "Drove forward." : "Reversed.";
            return Result(message);
        }

        private ActionResult DoRest()
        {
            // Rest does not consume fuel.
            _driver.Rest();
            return Result("Driver rested.");
        }

        private ActionResult DoRefuel()
        {
            _car.Refuel();
            _driver.IncreaseFatigue();
            return Result("Refueled to 100%.");
        }

        private ActionResult Result(string message)
        {
            return new ActionResult(
                message,
                _car.Direction,
                _car.Fuel,
                _driver.Fatigue,
                _driver.WarningText);
        }
    }
}
