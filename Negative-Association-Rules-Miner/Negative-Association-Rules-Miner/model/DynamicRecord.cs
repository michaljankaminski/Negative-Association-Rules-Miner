using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.model
{
    internal class DynamicRecord : IDataSourceModel
    {
        private IDictionary<string,object> Record { get; set; }
    }
}
