using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class RecordsDataSet
    {
        public List<string> Headers { get; set; }
        public IList<DynamicRecord> Records { get; set; }
        public RecordsDataSet(List<string> headers, IList<DynamicRecord> records)
        {
            Headers = headers;
            Records = records;
        }
    }
}
