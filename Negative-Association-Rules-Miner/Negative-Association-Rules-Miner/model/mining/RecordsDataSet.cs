using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class RecordsDataSet
    {
        public IList<string> Headers { get; set; }
        public IList<DynamicRecord> Records { get; set; }
        public RecordsDataSet(List<string> headers, IList<DynamicRecord> records)
        {
            Headers = headers;
            Records = records;
        }

        public RecordsDataSet CopyExcludingItems(IList<string> itemsToExclude)
        {
            List<int> newHeadersIndexes = new List<int>();
            List<string> newHeaders = new List<string>(Headers);
            List<DynamicRecord> newRecords = new List<DynamicRecord>(Records);

            foreach (var item in itemsToExclude)
            {
                for (int i = 0; i < Headers.Count; i++)
                {
                    if (item == Headers[i])
                        newHeadersIndexes.Add(i);
                }
            }

            for(int i = newHeadersIndexes.Count - 1; i >= 0; i--)
            {
                newHeaders.RemoveAt(newHeadersIndexes[i]);
                newRecords.RemoveAt(newHeadersIndexes[i]);
            }

            return new RecordsDataSet(newHeaders, newRecords);
        }

        public RecordsDataSet CopyIncludingItems(IList<string> itemsToInclude)
        {
            List<int> newHeadersIndexes = new List<int>();
            List<string> newHeaders = new List<string>(Headers);
            List<DynamicRecord> newRecords = new List<DynamicRecord>(Records);


            for (int i = 0; i < itemsToInclude.Count; i++)
            {
                if (!Headers.Contains(itemsToInclude[i]))
                    newHeadersIndexes.Add(i);
            }

            for (int i = newHeadersIndexes.Count - 1; i >= 0; i--)
            {
                newHeaders.RemoveAt(newHeadersIndexes[i]);
                newRecords.RemoveAt(newHeadersIndexes[i]);
            }

            return new RecordsDataSet(newHeaders, newRecords);
        }
    }
}
