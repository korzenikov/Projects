using System;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class SccCalculatorTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("SCC.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                int length = 875715;
                var adjacencyList = new Node[length];

                for (int i = 0; i < length; i++)
                {
                    adjacencyList[i] = new Node();
                }

                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var numbers = parts.Select(x => int.Parse(x) - 1).ToArray();
                    var from = numbers[0];
                    var to = numbers[1];
                    if (from != to)
                    {
                        adjacencyList[from].OutEdges.Add(to);
                        adjacencyList[to].InEdges.Add(from);
                    }
                }

                var calculator = new SccCalculator(adjacencyList);

                var components = calculator.GetSccs().ToArray();
                var top5Components = components.OrderByDescending(x => x.Length).Take(5).ToArray();
                var result = string.Join(",", top5Components.Select(x => x.Length));
                writer.WriteLine(result);
            }
        }
    }
}
