using System;
using System.Collections.Generic;
using System.Linq;
using Negative_Association_Rules_Miner.model;

namespace Negative_Association_Rules_Miner
{
    interface IMinerHelper
    {
        double CalculateSupport(IEnumerable<IEnumerable<string>> transactionsSet, List<string> items, int numberOfRecords);

        IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
            where T : IComparable;

        IEnumerable<IEnumerable<string>> GetTransactionSet(IList<DynamicRecord> records, IList<string> headers);
    }

    class MinerHelper: IMinerHelper
    {
        public double CalculateSupport(IEnumerable<IEnumerable<string>> transactionsSet, List<string> items, int numberOfRecords)
        {
            var intersectionItems = transactionsSet.Where(t => t.Intersect(items).Count() == items.Count);
            double support = (double)intersectionItems.Count() / numberOfRecords; //FilteredDataSet.Records.Count

            return support;
        }

        public IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> list, int length)
            where T : IComparable
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetCombinations(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public IEnumerable<IEnumerable<string>> GetTransactionSet(IList<DynamicRecord> records, IList<string> headers)
        {
            return records
                .Select(r => r.Content
                    .Select((c, index) => new { value = c, index })
                    .Where(c => c.value.Equals("1"))
                    .Select(b => headers[b.index])); //FilteredDataSet.Headers[b.index]
        }
    }
}
