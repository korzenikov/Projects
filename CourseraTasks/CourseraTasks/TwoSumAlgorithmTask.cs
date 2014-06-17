using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class TwoSumAlgorithmTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("algo1-programming_prob-2sum.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var numbers = GetNumbers(reader);
                var twoSumAlgorithm = new TwoSumAlgorithm(numbers);
                var total = Partitioner.Create(-10000, 10001).AsParallel().Select(range =>
                    {
                        int count = 0;
                        for (var i = range.Item1; i < range.Item2; i++)
                        {
                            if (twoSumAlgorithm.CheckSum(i))
                            {
                                count++;
                            }
                        }
                        return count;
                    }).Sum();

                writer.WriteLine(total);
            }
        }

        private static IEnumerable<long> GetNumbers(TextReader inputReader)
        {
            while (true)
            {
                string numberStr = inputReader.ReadLine();
                if (numberStr == null)
                {
                    break;
                }

                yield return long.Parse(numberStr);
            }
        }
    }
}
