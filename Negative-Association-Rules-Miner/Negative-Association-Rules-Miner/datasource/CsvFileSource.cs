using System.Collections.Generic;
using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.service;

namespace Negative_Association_Rules_Miner.datasource
{
    interface IDataSource
    {
        IList<IDataSourceModel> GetPredefinedSet(string path);
        IList<IDataSourceModel> GetURLCsv(string url);
    }
    class CsvFileSource : IDataSource
    {
        private readonly ICsvParser _csvParser;
        private readonly ICsvConverter _csvConverter;

        public CsvFileSource()
        {
            _csvParser = new CsvParser();
            _csvConverter = new CsvConverter();
        }

        public CsvFileSource(ICsvParser csvParser, ICsvConverter csvConverter)
        {
            _csvParser = csvParser;
            _csvConverter = csvConverter;
        }

        public IList<IDataSourceModel> GetPredefinedSet(string path)
        {
            throw new System.NotImplementedException();
        }

        public IList<IDataSourceModel> GetURLCsv(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
