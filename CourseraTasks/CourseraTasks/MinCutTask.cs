using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CourseraTasks
{
    public class MinCutTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("kargerMinCut.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var adjacencyList = new List<HashSet<int>>();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var numbers = parts.Skip(1).Select(x => int.Parse(x) - 1).ToArray();

                    adjacencyList.Add(new HashSet<int>(numbers));
                }

                int result = MinCut.GetMinCutN(adjacencyList.ToArray(), 50000);
                writer.WriteLine(result);
            }
        }
    }
}
