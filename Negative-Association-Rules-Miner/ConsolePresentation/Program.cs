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

            //try
            //{
            //    MinerConsole mc = new MinerConsole();
            //    mc.Start();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    return;
            //}
            //finally
            //{
            //    Console.WriteLine("Application has finished working. Press any key to exit.");
            //    Console.ReadKey();
            //}


            //return;
            MinerManager manager = new MinerManager();
            var items = new List<string>();
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
