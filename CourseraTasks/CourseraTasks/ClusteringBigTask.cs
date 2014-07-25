using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class ClusteringBigTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("InputFiles//clustering_big.txt"))
            using (var writer = new StreamWriter("output2.txt"))
            {
                var firstRow = reader.ReadLine();
                var firtsRowParts = firstRow.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int numberOfNodes = int.Parse(firtsRowParts[0]);
                int numberOfBits = int.Parse(firtsRowParts[1]);

                var numbers = new List<int>(numberOfNodes);
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var bits = parts.Select(x => byte.Parse(x, CultureInfo.InvariantCulture)).ToArray();

                    int number = GetNumber(bits);
                    numbers.Add(number);
                }

                int maxClusters = Clustering.GetMaxClusters(numbers, numberOfBits, 2);
                writer.WriteLine(maxClusters);
            }
        }

        private static int GetNumber(IEnumerable<byte> bits)
        {
            return bits.Aggregate(0, (acc, element) => 2 * acc + element);
        }
    }
}
