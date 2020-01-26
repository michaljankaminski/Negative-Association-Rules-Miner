using System;
using System.Collections.Generic;
using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.model.mining;
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

        public IEnumerable<string> ViewAvailableSources()
        {
            return _dataSourceRepository.ListSources();
        }

        public bool SelectSource(int option)
        {
            try
            {
                _miner.LoadItemSet(_dataSourceRepository.Get(option));
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return false;
            }
        }

        public bool ExcludeItems(IList<string> itemsToExclude)
        {
            try
            {
                _miner.ExcludeItemsInDataSet(itemsToExclude);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return false;
            }
        }

        public bool IncludeItems(IList<string> itemsToInclude)
        {
            try
            {
                _miner.IncludeItemsInDataSet(itemsToInclude);
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return false;
            }
        }

        public IEnumerable<Rule> FindRule(RuleParameters parameters)
        {
            _miner.Test();
            return _miner.FindNegativeRule(parameters);
        }
    }
}
