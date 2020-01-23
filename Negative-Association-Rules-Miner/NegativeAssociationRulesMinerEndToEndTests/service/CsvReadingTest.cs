using Microsoft.VisualStudio.TestTools.UnitTesting;
using Negative_Association_Rules_Miner.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negative_Association_Rules_Miner.service.Tests
{
    [TestClass()]
    public class CsvReadingTest
    {
        private readonly MinerManager minerManager;

        public CsvReadingTest()
        {
            minerManager = new MinerManager();
        }

        [TestMethod()]
        public void ConvertTest()
        {
            var res = minerManager.SelectSource(1);
            Assert.IsTrue(res);
        }
    }
}