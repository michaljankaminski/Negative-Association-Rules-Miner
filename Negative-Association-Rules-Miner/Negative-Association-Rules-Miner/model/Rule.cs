using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negative_Association_Rules_Miner.model
{
    /// <summary>
    /// Basic model of single association rule 
    /// </summary>
    class Rule
    {
        public int TransactionId { get; set; }
        public IEnumerable<Item> ItemSet
        {
            get { return ItemSet.OrderBy(i => i.Name); }
            set { ItemSet = value; } 
        }

    }
}
