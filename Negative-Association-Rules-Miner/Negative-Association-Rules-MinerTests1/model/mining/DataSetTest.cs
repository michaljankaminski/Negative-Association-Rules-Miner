using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Negative_Association_Rules_Miner.model.mining.Tests
{
    [TestClass()]
    public class DataSetTest
    {
        [TestMethod()]
        public void CopyIncludingItemsTest()
        {
            var dataSet = new RecordsDataSet
            {
                Headers = new List<string>
                {
                    "test1",
                    "test2",
                    "test3",
                    "test4",
                    "test5"
                },
                Records = new List<DynamicRecord>
                {
                    new DynamicRecord(),
                    new DynamicRecord(),
                    new DynamicRecord(),
                    new DynamicRecord(),
                    new DynamicRecord(),

                }
            };

            Assert.AreEqual(dataSet.Headers.Count, dataSet.Records.Count);

            var newColumns = new List<string>
            {
                "test2",
                "test3",
                "test4"
            };
            var newDataSet = dataSet.CopyIncludingItems(newColumns);
            Assert.AreEqual(newDataSet.Headers.Count, newColumns.Count);
            Assert.AreEqual(newDataSet.Records.Count, newColumns.Count);

            Assert.AreNotEqual(newDataSet.Headers.Count, dataSet.Headers.Count);
            Assert.AreNotEqual(newDataSet.Records.Count, dataSet.Records.Count);
        }

        [TestMethod()]
        public void CopyExcludingItemsTest()
        {
            var dataSet = new RecordsDataSet
            {
                Headers = new List<string>
                {
                    "test1",
                    "test2",
                    "test3",
                    "test4",
                    "test5"
                },
                Records = new List<DynamicRecord>
                {
                    new DynamicRecord(),
                    new DynamicRecord(),
                    new DynamicRecord(),
                    new DynamicRecord(),
                    new DynamicRecord(),

                }
            };

            Assert.AreEqual(dataSet.Headers.Count, dataSet.Records.Count);

            var columnsToExlude = new List<string>
            {
                "test2",
                "test3",
                "test4"
            };
            var newDataSet = dataSet.CopyExcludingItems(columnsToExlude);
            Assert.AreEqual(newDataSet.Headers.Count, dataSet.Headers.Count - columnsToExlude.Count);
            Assert.AreEqual(newDataSet.Records.Count, dataSet.Records.Count - columnsToExlude.Count);

            Assert.AreNotEqual(newDataSet.Headers.Count, dataSet.Headers.Count);
            Assert.AreNotEqual(newDataSet.Records.Count, dataSet.Records.Count);
        }
    }
}