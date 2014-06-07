using CourseraTasks.CSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CourseraTasks
{
    public class QuickSortTask
    {
        public void Run()
        {
              using (var reader = new StreamReader("QuickSort.txt"))
              using (var writer = new StreamWriter("output.txt"))
              {
                  var numbers = GetNumbers(reader).ToArray();
                  var result1 = QuickSort.SortAndCount(numbers.ToArray(), (arr, l, r) => l);
                  writer.WriteLine(result1);

                  var result2 = QuickSort.SortAndCount(numbers.ToArray(), (arr, l, r) => r);
                  writer.WriteLine(result2);

                  var result3 = QuickSort.SortAndCount(numbers.ToArray(), QuickSort.GetMedian);
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
