using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class RuleParameters
    {
        public double MinSupport { get; set; } = 0.1d;
        public double MinConfidence { get; set; } = 0.1d;
        public int MinLength { get; set; } = 2;
        public int MaxLength { get; set; } = 4;
        public double Lift { get; set; }
    }
}
