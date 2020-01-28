﻿using Negative_Association_Rules_Miner.datasource;
using Negative_Association_Rules_Miner.model.mining;
using System.Collections.Generic;

namespace Negative_Association_Rules_Miner.repository
{
    interface IDataSourceRepository
    {
        bool AddNewFile(string path);
        RecordsDataSet Get(int key);
        IList<DynamicRecord> GetCsvByUrl(string url);
        IList<DynamicRecord> GetCsvByPath(string path);
        IList<string> ListSources();
    }
    class DataSourceRepository: IDataSourceRepository
    {
        private readonly IDataSource _csvDataSource;

        public DataSourceRepository()
        {
            _csvDataSource = new CsvDataSource();
        }

        public IList<string> ListSources()
        {
            return _csvDataSource.GetAvailableSources();
        }

        public bool AddNewFile(string path)
        {
            return _csvDataSource.AddNewFile(path);
        }

        public RecordsDataSet Get(int key)
        {
            return _csvDataSource.GetPredefinedSet(key);
        }

        public IList<DynamicRecord> GetCsvByUrl(string url)
        {
            throw new System.NotImplementedException();
        }

        public IList<DynamicRecord> GetCsvByPath(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
