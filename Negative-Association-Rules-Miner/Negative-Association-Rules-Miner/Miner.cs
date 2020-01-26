using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Rule = Negative_Association_Rules_Miner.model.Rule;

namespace Negative_Association_Rules_Miner
{
    interface IMiner
    {
        void LoadItemSet(RecordsDataSet data);
        bool IsLoaded();
        IEnumerable<string> GetHeadersList();
        void Test();
        bool IncludeItemsInDataSet(IList<string> itemsToInclude);
        bool ExcludeItemsInDataSet(IList<string> itemsToExclude);
        IEnumerable<Rule> FindNegativeRule(RuleParameters initialParameters);
    }
    public class Miner : IMiner
    {
        private readonly IRuleFinder _ruleFinder;

        private RecordsDataSet DataSet { get; set; } = null;
        private RecordsDataSet FilteredDataSet { get; set; }

        public Miner()
        {
            _ruleFinder = new RuleFinder();
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
        public IEnumerable<Rule> FindNegativeRule(RuleParameters initialParameters)
        {
            double minSupport = initialParameters.MinSupport;
            double minConfidence = initialParameters.MinConfidence;
            List<Rule> rulesResult = new List<Rule>();

            List<Item> initialFrequentItemset = new List<Item>();

            var transactionsSet = GetTransactionSet(FilteredDataSet.Records);

            var headersList = DataSet.Headers;

            foreach (var header in headersList)
                if (CalculateSupport(transactionsSet, new List<string> { header }) >= minSupport)
                    initialFrequentItemset.Add(
                            new Item { Name = header }
                       );

            int rulesCounter = 0;
            for (int k = 2; k < headersList.Count; k++)
            {
                Logger.Log(k);
                // we are getting the candidates from 1-frequent itemset
                // against the traditional convention - that set is always generated 
                // from 1-frequent itemset
                //List<List<Item>> candidates = frequentItemsets.ElementAt(0);

                // next we need to join them with theirselves to create 
                // possible combinations 
                var kFrequentCandidates = GetCombinations(initialFrequentItemset, k).ToList();
                var kFrequentItemset = new List<List<Item>>();
                foreach (var subSet in kFrequentCandidates)
                {
                    //Logger.
                    //    Log(string.Format("New subset from {0}-frequent candidates: ",k));

                    //if (CalculateSupport(transactionsSet, subSet.Select(s => s.Name).ToList()) > minSupport)
                    //    kFrequentItemset.Add(subSet.ToList());

                    for (int i = 1; i < subSet.Count(); i++)
                    {
                        // we get all possible combinations 
                        var lhs = GetCombinations(subSet, i);
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
                            double unionSupport = CalculateSupport(transactionsSet, lhsEl
                                .Union(rhs)
                                .Select(l => l.Name)
                                .ToList());
                            double lhsSupport = CalculateSupport(transactionsSet, lhsEl
                                .Select(l => l.Name)
                                .ToList());
                            double rhsSupport = CalculateSupport(transactionsSet, rhs
                                .Select(l => l.Name)
                                .ToList());

                            double supportANotB = lhsSupport - unionSupport;
                            double supportNotAB = rhsSupport - unionSupport;

                            double aNotBConfidence = supportANotB / lhsSupport;
                            double notABConfidence = supportNotAB / (1 - lhsSupport);

                            double convictionANotB = rhsSupport / (1 - aNotBConfidence);
                            double convictionNotAB = (1 - rhsSupport) / (1 - notABConfidence);

                            if (supportANotB >= minSupport && convictionANotB <= 2.0)
                            {
                                Logger.Log(string.Format("{0} => ~~ {1}",
                                    string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    string.Join(" ,", rhs.Select(r => r.Name))));
                                rulesResult.Add(candidateRule);
                                rulesCounter++;
                            }
                            else if (supportNotAB >= minSupport && convictionNotAB <= 2.0)
                            {
                                Logger.Log(string.Format("~~ {0} => {1}",
                                    string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    string.Join(" ,", rhs.Select(r => r.Name))));
                                rulesResult.Add(candidateRule);
                                rulesCounter++;
                            }


                        }
                    }
                }

            }

            return rulesResult;
        }

        public double CalculateSupport(IEnumerable<IEnumerable<string>> transactionsSet, List<string> items)
        {
            var intersectionItems = transactionsSet.Where(t => t.Intersect(items).Count() == items.Count);
            double support = (double)intersectionItems.Count() / DataSet.Records.Count();

            return support;
        }

        private IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
            where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private IEnumerable<IEnumerable<string>> GetTransactionSet(IList<DynamicRecord> records)
        {
            return records
                .Select(r => r.Content
                    .Select((c, index) => new { value = c, index = index })
                    .Where(c => c.value.Equals("1"))
                    .Select(b => DataSet.Headers[b.index]));
        }

        public void Test()
        {

            double minSupport = 0.4;
            double minConfidence = 0;

            List<Item> initialFrequentItemset = new List<Item>();

            var transactionsSet = GetTransactionSet(DataSet.Records);

            var headersList = DataSet.Headers;

            foreach (var header in headersList)
                if (CalculateSupport(transactionsSet, new List<string> { header }) >= minSupport)
                    initialFrequentItemset.Add(
                            new Item { Name = header }
                       );
            int rulesCounter = 0; 
            for (int k = 2; k < headersList.Count ; k++)
            {
                Logger.Log(k);
                // we are getting the candidates from 1-frequent itemset
                // against the traditional convention - that set is always generated 
                // from 1-frequent itemset
                //List<List<Item>> candidates = frequentItemsets.ElementAt(0);

                // next we need to join them with theirselves to create 
                // possible combinations 
                var kFrequentCandidates = GetCombinations(initialFrequentItemset, k).ToList();
                var kFrequentItemset = new List<List<Item>>();
                foreach (var subSet in kFrequentCandidates)
                {
                    //Logger.
                    //    Log(string.Format("New subset from {0}-frequent candidates: ",k));

                    //if (CalculateSupport(transactionsSet, subSet.Select(s => s.Name).ToList()) > minSupport)
                    //    kFrequentItemset.Add(subSet.ToList());

                    for (int i = 1; i < subSet.Count(); i++)
                    {
                        // we get all possible combinations 
                        var lhs = GetCombinations(subSet, i);
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
                            double unionSupport = CalculateSupport(transactionsSet, lhsEl
                                .Union(rhs)
                                .Select(l => l.Name)
                                .ToList());
                            double lhsSupport = CalculateSupport(transactionsSet, lhsEl
                                .Select(l => l.Name)
                                .ToList());
                            double rhsSupport = CalculateSupport(transactionsSet, rhs
                                .Select(l => l.Name)
                                .ToList());
                            double supportANotB = lhsSupport - unionSupport;
                            double supportNotAB = rhsSupport - unionSupport;

                            double aNotBConfidence = supportANotB / lhsSupport;
                            double notABConfidence = supportNotAB / (1 - lhsSupport);

                            double convictionANotB = rhsSupport / (1 - aNotBConfidence);
                            double convictionNotAB = (1 - rhsSupport) / (1 - notABConfidence);
                            if (supportANotB >= minSupport && convictionANotB <= 2.0)
                            {
                                Logger.Log(string.Format("{0} => ~~ {1}",
                                    string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    string.Join(" ,", rhs.Select(r => r.Name))));
                                rulesCounter++;

                            }
                            else if(supportNotAB >= minSupport && convictionNotAB <= 2.0)
                            {
                                Logger.Log(string.Format("~~ {0} => {1}",
                                    string.Join(" ,", lhsEl.Select(r => r.Name)),
                                    string.Join(" ,", rhs.Select(r => r.Name))));
                                rulesCounter++;
                            }


                        }
                    }
                }

            }
        }


    }
}
