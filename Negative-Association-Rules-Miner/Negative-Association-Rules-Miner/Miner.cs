﻿using System;
using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.repository;
using System.Collections.Generic;
using System.Linq;

namespace Negative_Association_Rules_Miner
{
    interface IMiner
    {
        void LoadItemSet(IList<DynamicRecord> records);
        bool FilterItemSet();
        IEnumerable<Rule> FindNegativeRule(Item item);
    }
    public class Miner : IMiner
    {
        private readonly IDataSourceRepository _dataSourceRepository;
        private readonly IRuleFinder _ruleFinder;

        private List<DynamicRecord> _itemSet { get; set; }
        
        public Miner()
        {
            _dataSourceRepository = new DataSourceRepository();
            _ruleFinder = new RuleFinder();
        }

        public void LoadItemSet(IList<DynamicRecord> records)
        {
            _itemSet = records.ToList();
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
        public IEnumerable<Rule> FindNegativeRule(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
