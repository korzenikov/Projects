using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class SCCCalculatorTask
    {
        public void Run(TextReader inputReader, TextWriter outputReader)
        {
            int length = 875715;
            var adjacencyList = new Node[length];

            for (int i = 0; i < length; i++)
            {
                adjacencyList[i] = new Node();
            }

            while (true)
            {
                string row = inputReader.ReadLine();
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

            var calculator = new SCCCalculator(adjacencyList);

            var components = calculator.GetSCCs().ToArray();
            var top5Components = components.OrderByDescending(x => x.Length).Take(5).ToArray();
            var result = string.Join(",", top5Components.Select(x => x.Length));
            outputReader.WriteLine(result);
        }
    }
}
