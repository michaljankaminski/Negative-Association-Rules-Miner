using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negative_Association_Rules_Miner;
using Negative_Association_Rules_Miner.model;

namespace Negative_Association_Rules_MinerTests1
{
    [TestClass()]
    public class CsvHandlerTest
    {
        private readonly CsvHandler _csvHandler;
        public CsvHandlerTest()
        {
            _csvHandler = new CsvHandler();
        }

        [TestMethod()]
        public void ReadCsvFileWithModelTest()
        {
            var path = @"C:\workspace\Negative-Association-Rules-Miner\Negative-Association-Rules-Miner\DataSources\example.csv";
            var records = _csvHandler.ReadCsvFile<ExampleModel>(path);
            var items = records.ToList();
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count() > 0);
        }

        [TestMethod()]
        public void ReadCsvFileWithoutModelTest()
        {
            var path = @"C:\workspace\Negative-Association-Rules-Miner\Negative-Association-Rules-Miner\DataSources\example.csv";
            var records = _csvHandler.ReadCsvFileDynamic(path).ToList();
            foreach (var record in records)
            {
               Console.WriteLine("hehe");
            }
            Assert.IsNotNull(records);
            Assert.IsTrue(records.Any());
        }
    }
}