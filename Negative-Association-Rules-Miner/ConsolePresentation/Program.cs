using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Negative_Association_Rules_Miner;
using Negative_Association_Rules_Miner.model.mining;

namespace ConsolePresentation
{
    internal class Program
    {
        private static void Main()
        {
            //CsvParser handler = new CsvParser();
            //handler.Test();
            MinerManager manager = new MinerManager();
            List<string> items = new List<string>();
            items = manager.ViewAvailableSources().ToList();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            int w = 2;
            var parameters = new RuleParameters
            {
                MinConfidence = 0,
                MinSupport = 0.4
            };
            manager.SelectSource(0);
            manager.FindRule(parameters);
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
