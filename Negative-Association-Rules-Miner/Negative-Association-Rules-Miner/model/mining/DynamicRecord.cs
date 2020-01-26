using System;
using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner.model
{
    public class DynamicRecord : IDataSourceModel
    {
        //public IDictionary<string, object> Record { get; set; }

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
