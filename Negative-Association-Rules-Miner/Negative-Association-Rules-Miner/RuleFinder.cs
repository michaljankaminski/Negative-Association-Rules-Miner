using Negative_Association_Rules_Miner.model;
using System;
using System.Collections.Generic;

namespace Negative_Association_Rules_Miner
{
    interface IRuleFinder
    {
        IEnumerable<Rule> FindNegative(List<IDataSourceModel> dataSourceModels, Item itemToFind);
    }
    class RuleFinder : IRuleFinder
    {
        public IEnumerable<Rule> FindNegative(List<IDataSourceModel> dataSourceModels, Item itemToFind)
        {
            throw new NotImplementedException();
        }
    }
}
