using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Negative_Association_Rules_Miner.model;

namespace Negative_Association_Rules_Miner
{
    public class CsvHandler
    {
        public IEnumerable<ICsvModel> ReadCsvFile<ICsvModel>(string path)
            where ICsvModel : class
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                return csv.GetRecords<ICsvModel>().ToList();
            }
        }

        public IEnumerable<dynamic> ReadCsvFileDynamic(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                return csv.GetRecords<dynamic>().ToList();
            }
        }

        public void Test()
        {
            var path = @"C:\workspace\Negative-Association-Rules-Miner\Negative-Association-Rules-Miner\DataSources\example.csv";
            var records = ReadCsvFileDynamic(path).ToList();
            foreach (dynamic record in records)
            {
                Console.WriteLine(record[0]);   
            }
            Console.ReadKey();

        }
    }
}
