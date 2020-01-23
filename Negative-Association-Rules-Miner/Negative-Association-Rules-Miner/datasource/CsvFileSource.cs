using System.Collections.Generic;
using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.service;

namespace Negative_Association_Rules_Miner.datasource
{
    interface IDataSource
    {
        IList<IDataSourceModel> GetPredefinedSet(int key);
        IList<IDataSourceModel> GetCustom(string url);
    }
    class CsvFileSource : IDataSource
    {
        private readonly ICsvParser _csvParser;
        private readonly ICsvConverter _csvConverter;
        private readonly IFileStorage _fileStorage;

        public CsvFileSource()
        {
            _csvParser = new CsvParser();
            _csvConverter = new CsvConverter();
            _fileStorage = new FileStorage();
        }

        public CsvFileSource(ICsvParser csvParser, ICsvConverter csvConverter)
        {
            _csvParser = csvParser;
            _csvConverter = csvConverter;
        }

        public IList<IDataSourceModel> GetPredefinedSet(int key)
        {
            throw new System.NotImplementedException();
        }

        public IList<IDataSourceModel> GetCustom(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
