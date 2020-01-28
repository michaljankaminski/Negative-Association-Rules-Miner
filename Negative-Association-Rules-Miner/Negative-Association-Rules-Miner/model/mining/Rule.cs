using System;
using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner.model.mining
{
    /// <summary>
    /// Basic model of single association rule 
    /// </summary>
    public class Rule
    {
        public int TransactionId { get; set; }

        private IList<Item> _leftItemList { get; set; }
        private IList<Item> _rightItemList { get; set; }

        public RuleType Type { get; set; }

        public double Support { get; set; }
        public double Confidence { get; set; }

        public IEnumerable<Item> LeftItemSet
        {
            get { return _leftItemList; }
            set { _leftItemList = value.ToList(); }
        }
        public IEnumerable<Item> RightItemSet
        {
            get { return _rightItemList; }
            set { _rightItemList = value.ToList(); }
        }

        public Tuple<IList<Item>, IList<Item>> SingleRule
        {
            get
            {
                return new Tuple<IList<Item>, IList<Item>>(_leftItemList,_rightItemList);
            }
        }

    }
}
