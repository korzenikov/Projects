using System.IO;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class BigKnapsackSolverTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("InputFiles//knapsack_big.txt"))
            using (var writer = new StreamWriter("output2.txt"))
            {
                var knapsack = KnapsackReader.GetKnapsack(reader);
                var solver = new BigKnapsackSolver(knapsack);
                var maxCost = solver.GetMaxCost();
                writer.WriteLine(maxCost);
            }
        }
    }
}