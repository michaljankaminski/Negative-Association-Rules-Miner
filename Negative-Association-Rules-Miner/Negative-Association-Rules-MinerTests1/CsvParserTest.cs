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
        private readonly CsvParser _csvHandler;
        public CsvHandlerTest()
        {
            _csvHandler = new CsvParser();
        }

        [TestMethod()]
        public void ReadCsvFileWithModelTest()
        {
            var path = @"C:\workspace\Negative-Association-Rules-Miner\Negative-Association-Rules-Miner\DataSources\example.csv";
            var records = _csvHandler.Parse<ExampleModel>(path).ToList();
            Assert.IsNotNull(records);
            Assert.IsTrue(records.Any());
        }

        [TestMethod()]
        public void ReadCsvFileWithoutModelTest()
        {
            var path = @"C:\workspace\Negative-Association-Rules-Miner\Negative-Association-Rules-Miner\DataSources\example.csv";
            var records = _csvHandler.ParseDynamic(path).ToList();
            foreach (var record in records)
            {
               Console.WriteLine("hehe");
            }
            Assert.IsNotNull(records);
            Assert.IsTrue(records.Any());
        }
    }
}