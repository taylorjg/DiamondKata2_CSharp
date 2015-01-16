using System;
using System.Diagnostics.CodeAnalysis;
using DiamondLib;

namespace DiamondApp
{
    internal class Program
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private static void Main(string[] args)
        {
            var inputChar = (args.Length > 0 && args[0].Length > 0) ? args[0][0] : 'P';
            Diamond.GenerateLines(inputChar).ForEach(Console.WriteLine);
        }
    }
}
