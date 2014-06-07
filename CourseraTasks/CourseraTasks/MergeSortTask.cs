using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class MergeSortTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("IntegerArray.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var numbers = GetNumbers(reader).ToArray();
                var result = MergeSort.CountInversions(numbers);
                writer.WriteLine(result);
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
