﻿using Negative_Association_Rules_Miner;
using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Linq;

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
            var items = manager.ViewAvailableSources().ToList();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            var parameters = new RuleParameters
            {
                MinConfidence = 0,
                MinSupport = 0.05,
                MaxLength = 4,
                MinLength = 3
            };
            MinerConsole mc = new MinerConsole();
            manager.SelectSource(0);
            manager.GetObservableRulesCollection().CollectionChanged += mc.MinerConsole_CollectionChanged;
            manager.FindRule(parameters);
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
