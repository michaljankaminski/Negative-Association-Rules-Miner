using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.model
{
    class DynamicRecord : IDataSourceModel
    {
        private IDictionary<string,object> Record { get; set; }
    }
}
