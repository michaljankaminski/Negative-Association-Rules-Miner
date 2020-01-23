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
    public class FileStorageTest
    {
        private readonly IFileStorage _fileStorage;
        public FileStorageTest()
        {
            _fileStorage = new FileStorage();
        }
        [TestMethod()]
        public void GetLocalFilesTest()
        {
            var files = _fileStorage.GetFiles();
            Assert.IsNotNull(files);
        }

        [TestMethod()]
        public void GetLocalFilesWhichExistTest()
        {
            var files = _fileStorage.GetFiles();
            Assert.IsTrue(files.Count >= 2);
        }
    }
}