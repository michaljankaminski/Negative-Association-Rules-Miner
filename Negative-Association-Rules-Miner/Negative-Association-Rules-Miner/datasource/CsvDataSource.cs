﻿using System.Collections.Generic;
using System.IO;
using Negative_Association_Rules_Miner.model;
using Negative_Association_Rules_Miner.service;

namespace Negative_Association_Rules_Miner.datasource
{
    interface IDataSource
    {
        IList<string> GetAvailableSources();
        IList<DynamicRecord> GetPredefinedSet(int key);
        IList<DynamicRecord> GetCustom(string url);
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

        public IList<DynamicRecord> GetPredefinedSet(int key)
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

        public IList<DynamicRecord> GetCustom(string url)
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
