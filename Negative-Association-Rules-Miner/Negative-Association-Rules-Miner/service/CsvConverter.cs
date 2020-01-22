using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negative_Association_Rules_Miner.service
{
    interface ICsvConverter
    {
        IList<IDictionary<string, object>> Convert(IEnumerable<dynamic> records);
    }
    class CsvConverter:ICsvConverter
    {
        public IList<IDictionary<string, object>> Convert(IEnumerable<dynamic> records)
        {
            IList <IDictionary<string, object>> convertedItems = new List<IDictionary<string, object>>();

            foreach (var record in records)
            {
                IDictionary<string, object> propertyValues = record;
                convertedItems.Add(record);
                foreach (var property in propertyValues.Keys)
                {
                    Console.WriteLine(string.Format("{0} : {1}", property, propertyValues[property]));
                }
            }

            return convertedItems;
        }
    }
}
