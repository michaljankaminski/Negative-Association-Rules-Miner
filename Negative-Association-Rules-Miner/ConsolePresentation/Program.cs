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
            CsvHandler handler = new CsvHandler();
            handler.Test();
            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
