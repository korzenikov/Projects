using System.IO;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class ClusteringTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("InputFiles//clustering1.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var graph = DirectedWeightedGraphReader.GetGraph(reader);
                var maxSpacing = Clustering.GetMaxSpacing(graph, 4);
                writer.WriteLine(maxSpacing);
            }
        }
    }
}
