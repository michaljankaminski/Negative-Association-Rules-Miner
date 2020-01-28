using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class DynamicRecord : IDataSourceModel
    {
        public IList<object> Content { get; set; }

        public DynamicRecord()
        {

        }

        public DynamicRecord(IDictionary<string, object> record)
        {
            Content = record.Values.ToList();
        }
    }
}
