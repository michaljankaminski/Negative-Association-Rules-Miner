using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Negative_Association_Rules_Miner.service
{
    public interface ICsvParser
    {
        IEnumerable<ICsvModel> Parse<ICsvModel>(string path) 
            where ICsvModel : class;
        IEnumerable<dynamic> ParseDynamic(string path);
    }

    public class CsvParser:ICsvParser
    {
        public IEnumerable<ICsvModel> Parse<ICsvModel>(string path)
        where ICsvModel:class
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                return csv.GetRecords<ICsvModel>().ToList();
            }
        }

        public IEnumerable<dynamic> ParseDynamic(string path)
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
            var records = ParseDynamic(path);
            foreach (var record in records)
            {
                IDictionary<string, object> propertyValues = record;
                foreach (var property in propertyValues.Keys)
                {
                    Console.WriteLine(string.Format("{0} : {1}", property, propertyValues[property]));
                }
            }
            Console.ReadKey();

        }
    }
}
