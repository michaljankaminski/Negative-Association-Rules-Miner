using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class RecordsDataSet
    {
        public RecordHeaders Headers { get; set; }
        public IList<DynamicRecord> Records { get; set; }

        public RecordsDataSet(RecordHeaders headers, IList<DynamicRecord> records)
        {
            Headers = headers;
            Records = records;
        }
    }
}
