using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.model
{
    public class DynamicRecord : IDataSourceModel
    {
        private IDictionary<string,object> Record { get; set; }

        public DynamicRecord(IDictionary<string, object> record)
        {
            Record = record;
        }
    }
}
