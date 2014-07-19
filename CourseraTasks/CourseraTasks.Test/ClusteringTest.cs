using CourseraTasks.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CourseraTasks.Test
{
    [TestClass]
    public class ClusteringTest
    {
        [TestMethod]
        public void GetMaxSpacingTest()
        {
            var graph = new DirectedWeightedGraph();
            
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 7);

            graph.AddEdge(1, 0, 1);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);

            graph.AddEdge(2, 0, 6);
            graph.AddEdge(2, 1, 8);
            graph.AddEdge(2, 3, 2);

            graph.AddEdge(3, 0, 7);
            graph.AddEdge(3, 1, 5);
            graph.AddEdge(3, 2, 2);

           Clustering.GetMaxSpacing(graph, 4).Should().Be(1);
           Clustering.GetMaxSpacing(graph, 3).Should().Be(2);
           Clustering.GetMaxSpacing(graph, 2).Should().Be(5);
        }
    }
}
