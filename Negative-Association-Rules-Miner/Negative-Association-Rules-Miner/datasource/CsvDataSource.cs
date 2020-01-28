using Negative_Association_Rules_Miner.model.mining;
using Negative_Association_Rules_Miner.service;
using System;
using System.Collections.Generic;
using System.IO;

namespace Negative_Association_Rules_Miner.datasource
{
    interface IDataSource
    {
        IList<string> GetAvailableSources();
        bool AddNewFile(string path);
        RecordsDataSet GetPredefinedSet(int key);
        RecordsDataSet GetCustom(string url);
    }
    class CsvDataSource : IDataSource
    {
        private readonly ICsvParser _csvParser;
        private readonly ICsvConverter _csvConverter;
        private readonly IFileStorage _fileStorage;

        public CsvDataSource()
        {
            _csvParser = new CsvParser();
            _csvConverter = new CsvConverter();
            _fileStorage = new FileStorage();
        }

        public CsvDataSource(ICsvParser csvParser, ICsvConverter csvConverter)
        {
            _csvParser = csvParser;
            _csvConverter = csvConverter;
        }

        public IList<string> GetAvailableSources()
        {
            List<string> files = new List<string>();

            foreach (var file in _fileStorage.GetFiles())
            {
                files.Add("Key: " + file.Key + ", Path: " + file.Path);
            }

            return files;
        }

        public bool AddNewFile(string path)
        {
            try
            {
                var numberOfFiles = _fileStorage.GetNumberOfFiles();
                _fileStorage.AddNewFile(path);
                if (numberOfFiles == _fileStorage.GetNumberOfFiles())
                    return false;
                return true;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                return false;
            }

        }

        public RecordsDataSet GetPredefinedSet(int key)
        {
            foreach (var file in _fileStorage.GetFiles())
            {
                if (file.Key == key && CheckIfCsv(file.Path))
                {
                    return _csvConverter.Convert(_csvParser.ParseDynamic(file.Path));
                }
            }

            return null;
        }

        public RecordsDataSet GetCustom(string url)
        {
            throw new System.NotImplementedException();
        }

        private bool CheckIfCsv(string path)
        {
            if (Path.GetExtension(path) == ".csv")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
