using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class KnapsackSolverTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("InputFiles//knapsack1.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var knapsack = KnapsackReader.GetKnapsack(reader);
                var maxCost = KnapsackSolver.GetMaxCost(knapsack);
                writer.WriteLine(maxCost);
            }
        }
    }
}
