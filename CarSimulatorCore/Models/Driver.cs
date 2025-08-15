using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Models
{
    public  class Driver
    {
        public string Name { get; private set; }
        public int Fatigue { get; private set; }
        public FatigueRules Rules { get; }

        public Driver(string name, FatigueRules rules, int initialFatigue = 0)
        {
            Name = string.IsNullOrWhiteSpace(name) ? "Unknown Driver" : name.Trim();
            Rules = rules ?? throw new ArgumentNullException(nameof(rules));
            Rules.Validate();
            Fatigue = Math.Max(0, initialFatigue);
        }

        public void IncreaseFatigue(int amount = 1)
            => Fatigue = Math.Min(Rules.MaxValue, Fatigue + Math.Max(0, amount));

        public void Rest()
            => Fatigue = Math.Max(0, Fatigue - Rules.RestRecovery);

        public bool IsWarning => Fatigue >= Rules.WarningThreshold && Fatigue < Rules.MaxValue;
        public bool IsMax => Fatigue >= Rules.MaxValue;

        public string? WarningText =>
            IsMax ? "ALERT: Driver is at MAX fatigue. Take an urgent rest!"
          : IsWarning ? "Warning: Driver is getting tired. Consider a rest."
          : null;

        public void Rename(string name)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name.Trim();
        }
    }
}