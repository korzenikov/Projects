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
            var result1 = QuickSort.SortAndCount(numbers.ToArray(), (arr, l, r) => l);
            outputReader.WriteLine(result1);

            var result2 = QuickSort.SortAndCount(numbers.ToArray(), (arr, l, r) => r);
            outputReader.WriteLine(result2);

            var result3 = QuickSort.SortAndCount(numbers.ToArray(),  QuickSort.GetMedian);
            outputReader.WriteLine(result3);
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
