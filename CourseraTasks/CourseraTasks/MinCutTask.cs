using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class MinCutTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("kargerMinCut.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var adjacencyList = new List<IEnumerable<int>>();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var numbers = parts.Skip(1).Select(x => int.Parse(x, CultureInfo.InvariantCulture) - 1).ToArray();

                    adjacencyList.Add(numbers);
                }

                int result = Enumerable.Repeat(0, 250).Select(_ => MinCut.GetMinCut(adjacencyList)).Min();

                writer.WriteLine(result);
            }
        }
    }
}
