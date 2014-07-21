using System;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class PrimsAlgorithmTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("edges.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var graph = DirectedWeightedGraphReader.GetGraph(reader);

                var length = PrimsAlgorithm.GetMinumumSpanningTreeLength(graph);
                writer.WriteLine(length);
            }
        }
    }
}
