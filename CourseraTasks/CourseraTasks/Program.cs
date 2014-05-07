using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CourseraTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TextReader inputReader = Console.In;
            TextWriter outputReader = Console.Out;
            var numbers = GetNumbers(inputReader).ToArray();
            var result = MergeSort.CountInversions(numbers);
            outputReader.WriteLine(result);
        }

        private static IEnumerable<int> GetNumbers(TextReader inputReader)
        {
            while (true)
            {
                string numberStr = inputReader.ReadLine();
                if (numberStr == null)
                {
                    break;
                }
 
                yield return int.Parse(numberStr);
            }
        }
    }
}
