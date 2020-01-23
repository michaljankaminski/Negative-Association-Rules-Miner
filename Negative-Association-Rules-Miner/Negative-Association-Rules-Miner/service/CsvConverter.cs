using System;
using System.Collections.Generic;
using System.Linq;
using Negative_Association_Rules_Miner.model;

namespace Negative_Association_Rules_Miner.service
{
    interface ICsvConverter
    {
        IList<DynamicRecord> Convert(IEnumerable<dynamic> records);
    }
    class CsvConverter:ICsvConverter
    {
        public IList<DynamicRecord> Convert(IEnumerable<dynamic> records)
        {
            IList <DynamicRecord> convertedItems = new List<DynamicRecord>();

            foreach (var record in records.ToList())
            {
                convertedItems.Add(new DynamicRecord(record));
                //foreach (var property in propertyValues.Keys)
                //{
                //    Console.WriteLine(string.Format("{0} : {1}", property, propertyValues[property]));
                //}
            }

            return convertedItems;
        }
    }
}
