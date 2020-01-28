using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Negative_Association_Rules_Miner.mining;
using Negative_Association_Rules_Miner.model.mining;
using Negative_Association_Rules_Miner.repository;

namespace Negative_Association_Rules_Miner.output
{
    public class MinerManager
    {
        private readonly IMiner _miner;
        private readonly IDataSourceRepository _dataSourceRepository;
        private readonly ObservableCollection<Rule> _setOfFoundRules;
        public MinerManager()
        {
            _setOfFoundRules = new ObservableCollection<Rule>();
            _miner = new Miner(_setOfFoundRules);
            _dataSourceRepository = new DataSourceRepository();

        }

        public IEnumerable<string> ViewAvailableSources()
        {
            return _dataSourceRepository.ListSources();
        }

        public bool AddNewSource(string path)
        {
            return _dataSourceRepository.AddNewFile(path);
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

        public IEnumerable<string> GetListOfHeaders()
        {
            return _miner.GetHeadersList();
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
            return _miner.FindNegativeRuleFirstApproach(parameters);
        }

        public IEnumerable<Rule> FindRuleSecondApproach(RuleParameters parameters)
        {
            return _miner.FindNegativeRuleSecondApproach(parameters);
        }

        public ObservableCollection<Rule> GetObservableRulesCollection()
        {
            return this._setOfFoundRules;
        }
    }
}
