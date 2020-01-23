using System;
using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Negative_Association_Rules_Miner.model.mining;
using Rule = Negative_Association_Rules_Miner.model.Rule;

namespace Negative_Association_Rules_Miner
{
    interface IMiner
    {
        void LoadItemSet(RecordsDataSet data);
        bool FilterItemSet();
        IEnumerable<Rule> FindNegativeRule(List<Item> items, Params initialParameters);
    }
    public class Miner : IMiner
    {
        private readonly IDataSourceRepository _dataSourceRepository;
        private readonly IRuleFinder _ruleFinder;

        private RecordsDataSet DataSet { get; set; }
        
        public Miner()
        {
            _dataSourceRepository = new DataSourceRepository();
            _ruleFinder = new RuleFinder();
        }

        public void LoadItemSet(RecordsDataSet data)
        {
            DataSet = DataSet;
            Logger.Log(data.Records.Count);
            Logger.Log(data.Headers.Headers.Count);
        }
        /// <summary>
        /// Method used to filtering the whole itemset - removing 
        /// all records not connected with interesting item
        /// </summary>
        /// <returns></returns>
        public bool FilterItemSet()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Method used for finding proper negative association rules
        /// </summary>
        /// <param name="item">
        /// Single item base on which we would like to find
        /// corresponding set of negative association rules
        /// </param>
        /// <returns>Set of rules</returns>
        public IEnumerable<Rule> FindNegativeRule(List<Item> items, Params initialParameters)
        {
            throw new NotImplementedException();
            //_ruleFinder.FindNegative()
        }
    }
}
