using System.Collections.Generic;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class QuicksortTask
    {
        public void Run()
        {
              using (var reader = new StreamReader("QuickSort.txt"))
              using (var writer = new StreamWriter("output.txt"))
              {
                  var numbers = GetNumbers(reader).ToArray();
                  var result1 = Quicksort.SortAndCount(numbers.ToArray(), (arr, l, r) => l);
                  writer.WriteLine(result1);

                  var result2 = Quicksort.SortAndCount(numbers.ToArray(), (arr, l, r) => r);
                  writer.WriteLine(result2);

                  var result3 = Quicksort.SortAndCount(numbers.ToArray(), Quicksort.GetMedian);
                  writer.WriteLine(result3);
              }
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
