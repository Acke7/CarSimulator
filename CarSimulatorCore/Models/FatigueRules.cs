using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSimulatorCore.Models
{
    public  class FatigueRules
    {
        public int WarningThreshold { get; }
        public int MaxValue { get; }
        public int RestRecovery { get; }

        public FatigueRules(int warningThreshold, int maxValue, int restRecovery = 2)
        {
            WarningThreshold = warningThreshold;
            MaxValue = maxValue;
            RestRecovery = restRecovery;
        }

        public void Validate()
        {
            if (WarningThreshold < 0 || MaxValue <= 0 || WarningThreshold > MaxValue)
                throw new ArgumentException("Invalid fatigue thresholds.");
            if (RestRecovery <= 0)
                throw new ArgumentException("RestRecovery must be > 0.");
        }
    }
}
