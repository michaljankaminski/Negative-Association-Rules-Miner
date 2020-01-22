using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner.model
{
    /// <summary>
    /// Basic model of single association rule 
    /// </summary>
    public class Rule
    {
        public int TransactionId { get; set; }

        private List<IDataSourceModel> _itemList { get; set; }

        public IEnumerable<IDataSourceModel> ItemSet
        {
            get { return _itemList; }
            set { _itemList = value.ToList(); } 
        }

    }
}
