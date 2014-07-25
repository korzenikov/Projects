using System.IO;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class PrimsAlgorithmTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("edges.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var graph = DirectedWeightedGraphReader.GetGraph(reader);

                var length = PrimsAlgorithm.GetMinimumSpanningTreeLength(graph);
                writer.WriteLine(length);
            }
        }
    }
}
