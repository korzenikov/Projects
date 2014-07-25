using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class TwoSumAlgorithmTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("algo1-programming_prob-2sum.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var numbers = GetNumbers(reader);
                var twoSumAlgorithm = new TwoSumAlgorithm(numbers);

                var sw1 = Stopwatch.StartNew();

                var total1 = Partitioner.Create(-10000, 10001).AsParallel().Select(
                    range =>
                    {
                        int count = 0;
                        for (var i = range.Item1; i < range.Item2; i++)
                        {
                            if (twoSumAlgorithm.CanGetSum(i))
                            {
                                count++;
                            }
                        }

                        return count;
                    }).Sum();

                sw1.Stop();

                writer.WriteLine(total1);
                writer.WriteLine(sw1.ElapsedMilliseconds);

                var sw2 = Stopwatch.StartNew();
                var total2 = Partitioner.Create(-10000, 10001).AsParallel().Select(
                    range => twoSumAlgorithm.CheckSequence(range.Item1, range.Item2)).Sum();
                sw2.Stop();

                writer.WriteLine(total2);
                writer.WriteLine(sw2.ElapsedMilliseconds);
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
