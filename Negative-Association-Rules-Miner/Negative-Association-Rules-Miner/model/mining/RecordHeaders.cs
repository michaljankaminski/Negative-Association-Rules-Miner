using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner.model.mining
{
    public class RecordHeaders
    {
        public List<string> Headers { get; set; }

        public RecordHeaders(IList<string> headers)
        {
            Headers = headers.ToList();
        }
    }
}
