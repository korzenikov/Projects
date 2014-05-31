using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class MinCutTask
    {
        public void Run(TextReader inputReader, TextWriter outputReader)
        {
            var adjacencyList = new List<int[]>();
            while (true)
            {
                string row = inputReader.ReadLine();
                if (row == null)
                {
                    break;
                }

                var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var numbers = parts.Skip(1).Select(x => int.Parse(x) - 1).ToArray();
                adjacencyList.Add(numbers);
            }
            int result = MinCut.GetMinCutN(adjacencyList.ToArray(), 100);
            Console.WriteLine(result);
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
