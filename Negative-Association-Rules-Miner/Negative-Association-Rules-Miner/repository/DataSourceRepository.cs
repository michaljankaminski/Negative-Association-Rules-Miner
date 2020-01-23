using System.Collections.Generic;
using Negative_Association_Rules_Miner.model;

namespace Negative_Association_Rules_Miner.repository
{
    interface IDataSourceRepository
    {
        IList<IDataSourceModel> Get(int key);
        IList<IDataSourceModel> GetCsvByUrl(string url);
        IList<IDataSourceModel> GetCsvByPath(string path);
    }
    class DataSourceRepository: IDataSourceRepository
    {

        public IList<IDataSourceModel> Get(int key)
        {
            throw new System.NotImplementedException();
        }

        public IList<IDataSourceModel> GetCsvByUrl(string url)
        {
            throw new System.NotImplementedException();
        }

        public IList<IDataSourceModel> GetCsvByPath(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
