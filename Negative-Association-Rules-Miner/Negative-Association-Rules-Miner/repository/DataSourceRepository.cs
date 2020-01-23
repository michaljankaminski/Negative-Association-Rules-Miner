using System.Collections.Generic;
using Negative_Association_Rules_Miner.model;

namespace Negative_Association_Rules_Miner.repository
{
    interface IDataSourceRepository
    {
        IList<IDataSourceModel> Get(string path);
    }
    class DataSourceRepository: IDataSourceRepository
    {
        public IList<IDataSourceModel> Get(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
