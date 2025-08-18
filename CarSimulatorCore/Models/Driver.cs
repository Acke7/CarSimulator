using System;

namespace CarSimulatorCore.Models
{
    public class Driver
    {
        public string Name { get; private set; }
        public int Fatigue { get; private set; }
        public FatigueRules Rules { get; }

        public Driver(string name, FatigueRules rules, int initialFatigue = 0)
        {
            if (rules == null) throw new ArgumentNullException(nameof(rules));

            Name = string.IsNullOrWhiteSpace(name) ? "Unknown Driver" : name.Trim();
            Rules = rules;
            Rules.Validate();

            if (initialFatigue < 0) initialFatigue = 0;
            Fatigue = initialFatigue;
        }

        public void IncreaseFatigue(int amount = 1)
        {
            if (amount < 0) amount = 0;

            int next = Fatigue + amount;
            if (next > Rules.MaxValue) next = Rules.MaxValue;

            Fatigue = next;
        }

        public void Rest()
        {
            int next = Fatigue - Rules.RestRecovery;
            if (next < 0) next = 0;

            Fatigue = next;
        }

        public bool IsWarning
        {
            get
            {
                return Fatigue >= Rules.WarningThreshold && Fatigue < Rules.MaxValue;
            }
        }

        public bool IsMax
        {
            get
            {
                return Fatigue >= Rules.MaxValue;
            }
        }

        public string? WarningText
        {
            get
            {
                if (IsMax) return "ALERT: Driver is at MAX fatigue. Take an urgent rest!";
                if (IsWarning) return "Warning: Driver is getting tired. Consider a rest.";
                return null;
            }
        }

        public void Rename(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name.Trim();
            }
        }
    }
}
