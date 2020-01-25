using System;
using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner.model
{
    public class DynamicRecord : IDataSourceModel
    {
        //public IDictionary<string, object> Record { get; set; }

       public IList<object> Content { get; set; }
        

        public DynamicRecord(IDictionary<string, object> record)
        {
            Content = record.Values.ToList();
            //foreach(var rec in record.Values)
            //{
            //    Content.Add(Convert.ToBoolean(rec.ToString()));
            //}


            //Content = new List<bool>(record.Values.Select(l => Convert.ToBoolean(l)));
            //Content = new List<object>();
            //foreach (var property in record.Keys)
            //    Content.Add(record[property]);
        }
    }
}
