using System;
using System.Collections.Generic;
using Negative_Association_Rules_Miner.repository;

namespace Negative_Association_Rules_Miner
{
    public class MinerManager
    {
        private readonly IMiner _miner;
        private readonly IDataSourceRepository _dataSourceRepository;
        public MinerManager()
        {
            _miner = new Miner();
            _dataSourceRepository = new DataSourceRepository();
        }

        public IEnumerable<string> ViewAvaliableSources()
        {
            return _dataSourceRepository.ListSources();
        }

        public bool SelectSource(int option)
        {
            throw new NotImplementedException();
        }

        public string FindRule(string item)
        {
            throw new NotImplementedException();
        }
    }
}
