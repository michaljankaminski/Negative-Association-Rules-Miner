using System;
using System.IO;
using System.Reflection;
using Negative_Association_Rules_Miner;

namespace ConsolePresentation
{
    internal class Program
    {
        private static void Main()
        {
            CsvParser handler = new CsvParser();
            handler.Test();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
