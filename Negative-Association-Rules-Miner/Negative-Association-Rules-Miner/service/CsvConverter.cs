using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.model.mining;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Negative_Association_Rules_Miner.service
{
    interface ICsvConverter
    {
        RecordsDataSet Convert(IEnumerable<dynamic> records);
    }
    public class CsvConverter:ICsvConverter
    {
        public RecordsDataSet Convert(IEnumerable<dynamic> records)
        {
            List<string> headers = null;
            var listedRecords = records.ToList();

            if (listedRecords.Count > 0)
            {
                IDictionary<string, object> firstItem = listedRecords[0];
                headers = new List<string>(firstItem.Keys.ToList());
            }
            
            List<DynamicRecord> convertedItems = new List<DynamicRecord>();
            
            foreach (var record in listedRecords)
                convertedItems.Add(new DynamicRecord(record));
                

            return new RecordsDataSet(headers, convertedItems);
        }
    }
}
