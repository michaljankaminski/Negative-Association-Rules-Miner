using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Negative_Association_Rules_Miner.mining
{
    interface IMiner
    {
        void LoadItemSet(RecordsDataSet data);
        bool IsLoaded();
        IEnumerable<string> GetHeadersList();
        bool IncludeItemsInDataSet(IList<string> itemsToInclude);
        bool ExcludeItemsInDataSet(IList<string> itemsToExclude);
        IEnumerable<Rule> FindNegativeRuleFirstApproach(RuleParameters initialParameters);
        IEnumerable<Rule> FindNegativeRuleSecondApproach(RuleParameters initialParameters);

    }
    public class Miner : IMiner
    {
        private readonly IMinerHelper _minerHelper;

        private RecordsDataSet DataSet { get; set; } = null;
        private RecordsDataSet FilteredDataSet { get; set; }
        private ObservableCollection<Rule> setOfFoundRules;

        public Miner(ObservableCollection<Rule> foundRules)
        {
            _minerHelper = new MinerHelper();
            setOfFoundRules = foundRules;
        }

        public void LoadItemSet(RecordsDataSet data)
        {
            DataSet = data;
            FilteredDataSet = data;
        }

        public bool IsLoaded()
        {
            if (DataSet == null)
                return false;
            else
            {
                return true;
            }
        }

        public IEnumerable<string> GetHeadersList()
        {
            if (IsLoaded())
                return FilteredDataSet.Headers;
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Method used to filtering the whole itemset - removing 
        /// all records not connected with interesting item
        /// </summary>
        /// <returns></returns>

        public bool IncludeItemsInDataSet(IList<string> itemsToInclude)
        {
            try
            {
                if (DataSet != null)
                    FilteredDataSet = DataSet.CopyIncludingItems(itemsToInclude);
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return false;
            }
        }

        public bool ExcludeItemsInDataSet(IList<string> itemsToExclude)
        {
            try
            {
                if (DataSet != null)
                    FilteredDataSet = DataSet.CopyExcludingItems(itemsToExclude);
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Method used for finding proper negative association rules
        /// </summary>
        /// <param name="item">
        /// Single item base on which we would like to find
        /// corresponding set of negative association rules
        /// </param>
        /// <returns>Set of rules</returns>
        public IEnumerable<Rule> FindNegativeRuleFirstApproach(RuleParameters initialParameters)
        {
            int rulesCounter = 0;
            double minSupport = initialParameters.MinSupport;

            List<Rule> rulesResult = new List<Rule>();
            List<Item> initialFrequentItemset = new List<Item>();

            var transactionsSet = _minerHelper.GetTransactionSet(FilteredDataSet.Records, FilteredDataSet.Headers);

            var headersList = FilteredDataSet.Headers;

            foreach (var header in headersList)
                if (_minerHelper.CalculateSupport(transactionsSet, new List<string> { header }, FilteredDataSet.Records.Count) >= minSupport)
                    initialFrequentItemset.Add(
                            new Item { Name = header }
                       );
            
            for (int k = initialParameters.MinLength; k < initialParameters.MaxLength ; k++)
            {           
                // we are getting the candidates from 1-frequent itemset
                // against the traditional convention - that set is always generated 
                // from 1-frequent itemset

                // next we need to join them with theirselves to create 
                // possible combinations 
                var kFrequentCandidates = _minerHelper.GetCombinations(initialFrequentItemset, (k-1)*2).ToList();
                var kFrequentItemset = new List<List<Item>>();
                foreach (var subSet in kFrequentCandidates)
                {

                    if (_minerHelper.CalculateSupport(transactionsSet, subSet.Select(s => s.Name).ToList(), FilteredDataSet.Records.Count) >= minSupport)
                    {
                        for (int i = 1; i < subSet.Count(); i++)
                        {
                            // we get all possible combinations 
                            var lhs = _minerHelper.GetCombinations(subSet, i);
                            foreach (var lhsEl in lhs)
                            {
                                var rhs = subSet.Where(s => !lhsEl.Contains(s));
                                // for every candidate rule, we are suppose to check some 
                                // conditions such as : correlation coeff; support and confidence
                                // base on the results, we can easily classify single rule as a positive either negative
                                // We also use a 'conviction' parameter
                                double unionSupport = _minerHelper.CalculateSupport(transactionsSet, lhsEl
                                    .Union(rhs)
                                    .Select(l => l.Name)
                                    .ToList(), FilteredDataSet.Records.Count);
                                double lhsSupport = _minerHelper.CalculateSupport(transactionsSet, lhsEl
                                    .Select(l => l.Name)
                                    .ToList(), FilteredDataSet.Records.Count);
                                double rhsSupport = _minerHelper.CalculateSupport(transactionsSet, rhs
                                    .Select(l => l.Name)
                                    .ToList(), FilteredDataSet.Records.Count);

                                double supportANotB = lhsSupport - unionSupport;
                                double supportNotAB = rhsSupport - unionSupport;

                                double aNotBConfidence = supportANotB / lhsSupport;
                                double notABConfidence = supportNotAB / (1 - lhsSupport);

                                double convictionANotB = rhsSupport / (1 - aNotBConfidence);
                                double convictionNotAB = (1 - rhsSupport) / (1 - notABConfidence);

                                var candidateRule = new Rule
                                {
                                    LeftItemSet = lhsEl,
                                    RightItemSet = rhs,
                                };

                                if (supportANotB >= minSupport && convictionANotB <= 2.0)
                                {
                                    candidateRule.Support = supportANotB;
                                    candidateRule.Type = RuleType.RightNegative;
                                    //Logger.Log(string.Format("{0} => ~~ {1}",
                                    //    string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    //    string.Join(" ,", rhs.Select(r => r.Name))));
                                    setOfFoundRules.Add(candidateRule);
                                    rulesResult.Add(candidateRule);
                                    rulesCounter++;
                                }
                                else if (supportNotAB >= minSupport && convictionNotAB <= 2.0)
                                {
                                    candidateRule.Support = supportNotAB;
                                    candidateRule.Type = RuleType.RightNegative;
                                    //Logger.Log(string.Format("~~ {0} => {1}",
                                    //    string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    //    string.Join(" ,", rhs.Select(r => r.Name))));
                                    setOfFoundRules.Add(candidateRule);
                                    rulesResult.Add(candidateRule);
                                    rulesCounter++;
                                }


                            }
                        }
                    }
                }

            }
            return rulesResult;
        }
        public IEnumerable<Rule> FindNegativeRuleSecondApproach(RuleParameters initialParameters)
        {
            double minSupport = initialParameters.MinSupport;
            double minConfidence = initialParameters.MinConfidence;

            List<Rule> rulesResult = new List<Rule>();
            List<Item> initialFrequentItemset = new List<Item>();

            var transactionsSet = _minerHelper.GetTransactionSet(FilteredDataSet.Records, FilteredDataSet.Headers);
            var headersList = FilteredDataSet.Headers;

            foreach (var header in headersList)
                if (_minerHelper.CalculateSupport(transactionsSet, new List<string> { header }, FilteredDataSet.Records.Count) >= minSupport)
                    initialFrequentItemset.Add(
                            new Item { Name = header }
                       );

            int rulesCounter = 0;
            for (int k = initialParameters.MinLength; k < initialParameters.MaxLength; k++)
            {
                // we are getting the candidates from 1-frequent itemset
                // against the traditional convention - that set is always generated 
                // from 1-frequent itemset
                //List<List<Item>> candidates = frequentItemsets.ElementAt(0);

                // next we need to join them with theirselves to create 
                // possible combinations 
                var kFrequentCandidates = _minerHelper.GetCombinations(initialFrequentItemset, k).ToList();
                var kFrequentItemset = new List<List<Item>>();
                foreach (var subSet in kFrequentCandidates)
                {
                    if (_minerHelper.CalculateSupport(transactionsSet, subSet.Select(s => s.Name).ToList(), FilteredDataSet.Records.Count) >= minSupport)
                    {
                        for (int i = 1; i < subSet.Count(); i++)
                        {
                            // we get all possible combinations 
                            var lhs = _minerHelper.GetCombinations(subSet, i);
                            foreach (var lhsEl in lhs)
                            {
                                var rhs = subSet.Where(s => !lhsEl.Contains(s));
                                var candidateRule = new Rule
                                {
                                    LeftItemSet = lhsEl,
                                    RightItemSet = rhs
                                };
                                // for every candidate rule, we are suppose to check some 
                                // conditions such as : correlation coeff; support and confidence
                                // base on the results, we can easily classify single rule as a positive either negative
                                // We also use a 'conviction' parameter
                                double unionSupport = _minerHelper.CalculateSupport(transactionsSet, lhsEl
                                    .Union(rhs)
                                    .Select(l => l.Name)
                                    .ToList(), FilteredDataSet.Records.Count);
                                double lhsSupport = _minerHelper.CalculateSupport(transactionsSet, lhsEl
                                    .Select(l => l.Name)
                                    .ToList(), FilteredDataSet.Records.Count);
                                double rhsSupport = _minerHelper.CalculateSupport(transactionsSet, rhs
                                    .Select(l => l.Name)
                                    .ToList(), FilteredDataSet.Records.Count);

                                double supportANotB = lhsSupport - unionSupport;
                                double supportNotAB = rhsSupport - unionSupport;
                                double supportNotANotB = 1 - lhsSupport - unionSupport;

                                double aNotBConfidence = supportANotB / lhsSupport;
                                double notABConfidence = supportNotAB / (1 - lhsSupport);
                                double notANotBConfidence = supportNotANotB / (1 - lhsSupport);
                                double ABConfidence = unionSupport / lhsSupport;

                                if (notANotBConfidence >= minConfidence && supportNotANotB >= minSupport)
                                {
                                    candidateRule.Support = supportNotANotB;
                                    candidateRule.Confidence = notANotBConfidence;
                                    candidateRule.Type = RuleType.BothNegative;
                                    //Logger.Log(string.Format("¬ {0} => ¬ {1} [{2}]",
                                    //   string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    //   string.Join(" ,", rhs.Select(r => r.Name)),
                                    //   Math.Round(notANotBConfidence, 2)
                                    //   ));
                                    setOfFoundRules.Add(candidateRule);
                                    rulesCounter++;
                                    rulesResult.Add(candidateRule);
                                }
                                if (supportANotB >= minSupport && aNotBConfidence >= minConfidence)
                                {
                                    candidateRule.Support = supportANotB;
                                    candidateRule.Confidence = aNotBConfidence;
                                    candidateRule.Type = RuleType.RightNegative;
                                    //Logger.Log(string.Format("{0} => ¬ {1} [{2}]",
                                    //   string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    //   string.Join(" ,", rhs.Select(r => r.Name)),
                                    //   Math.Round(aNotBConfidence, 2)));
                                    setOfFoundRules.Add(candidateRule);
                                    rulesCounter++;
                                    rulesResult.Add(candidateRule);
                                }
                                if (supportNotAB >= minSupport && notABConfidence >= minConfidence)
                                {
                                    candidateRule.Support = supportNotAB;
                                    candidateRule.Confidence = aNotBConfidence;
                                    candidateRule.Type = RuleType.LeftNegative;
                                    //Logger.Log(string.Format("¬ {0} => {1} [{2}]",
                                    //   string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    //   string.Join(" ,", rhs.Select(r => r.Name)),
                                    //   Math.Round(notABConfidence, 2)
                                    //   ));
                                    setOfFoundRules.Add(candidateRule);
                                    rulesCounter++;
                                    rulesResult.Add(candidateRule);
                                }
                            }
                        }
                    }
                }

            }
            return rulesResult;
        }

    }
}
