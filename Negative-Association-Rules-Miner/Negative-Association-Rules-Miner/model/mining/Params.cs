using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class Params
    {
        public double Support { get; set; }
        public double Confidence { get; set; }
        public double Lift { get; set; }
    }
}
