﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Negative_Association_Rules_Miner;

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

            manager.SelectSource(1);
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
