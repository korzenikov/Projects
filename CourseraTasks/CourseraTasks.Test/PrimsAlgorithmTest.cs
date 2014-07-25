using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class PrimsAlgorithmTest
    {
        [TestMethod]
        public void GetMinimumSpanningTreeLengthTest()
        {
            var graph = new DirectedWeightedGraph();
            
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 4);
            graph.AddEdge(0, 3, 3);
            
            graph.AddEdge(1, 0, 1);
            graph.AddEdge(1, 3, 2);
            
            graph.AddEdge(2, 0, 4);
            graph.AddEdge(2, 3, 5);

            graph.AddEdge(3, 0, 3);
            graph.AddEdge(3, 1, 2);
            graph.AddEdge(3, 2, 5);

            var mstLength = PrimsAlgorithm.GetMinimumSpanningTreeLength(graph);
            mstLength.Should().Be(7);
        }
    }
}
