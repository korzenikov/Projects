using System.Collections.Generic;
using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class StronglyConnectedComponentsCalculatorTest
    {
        [TestMethod]
        public void DepthFirstSearchTest()
        {
            var graph1 = GetTestGraph();

            var calculator1 = new StronglyConnectedComponentsCalculator<int>(graph1);
            IEnumerable<int> vertices1 = calculator1.DepthFirstSearch(0, true);
            vertices1.Should().HaveCount(9);

            var calculator2 = new StronglyConnectedComponentsCalculator<int>(graph1);
            IEnumerable<int> vertices2 = calculator2.DepthFirstSearch(1, true);
            vertices2.Should().HaveCount(3);

            var graph2 = new DirectedGraph<int>();
            graph2.AddEdge(0, 1);
            graph2.AddEdge(0, 2);
            graph2.AddEdge(2, 1);

            var calculator3 = new StronglyConnectedComponentsCalculator<int>(graph2);
            IEnumerable<int> vertices3 = calculator3.DepthFirstSearch(0, false);
            vertices3.Should().HaveCount(3);
        }

        [TestMethod]
        public void DepthFirstSearchLoopTest()
        {
            var graph = GetTestGraph();

            var calculator1 = new StronglyConnectedComponentsCalculator<int>(graph);
            var result1 = calculator1.DepthFirstSearchLoop(graph.Keys, true).ToArray();
            result1.Length.Should().BeLessOrEqualTo(3);

            var calculator2 = new StronglyConnectedComponentsCalculator<int>(graph);
            var result2 = calculator2.DepthFirstSearchLoop(graph.Keys.Reverse(), true).ToArray();
            result2.Length.Should().BeLessOrEqualTo(3);
        }

        [TestMethod]
        public void GetStronglyConnectedComponentsTest()
        {
            var graph = GetTestGraph();

            var calculator = new StronglyConnectedComponentsCalculator<int>(graph);
            var components = calculator.GetStronglyConnectedComponents().ToArray();
            components.Should().HaveCount(3);
            components[0].Should().BeEquivalentTo(new[] { 0, 6, 3 });
            components[1].Should().BeEquivalentTo(new[] { 8, 5, 2 });
            components[2].Should().BeEquivalentTo(new[] { 7, 1, 4 });
        }

        [TestMethod]
        public void GetStronglyConnectedComponentsTest2()
        {
            var graph = new DirectedGraph<int>();
            graph.AddEdge(1, 3);
            graph.AddEdge(3, 5);
            graph.AddEdge(3, 9);
            graph.AddEdge(5, 7);
            graph.AddEdge(7, 1);

            var calculator = new StronglyConnectedComponentsCalculator<int>(graph);
            var components = calculator.GetStronglyConnectedComponents().ToArray();
            components.Should().HaveCount(2);
            components[0].Should().BeEquivalentTo(new[] { 9 });
            components[1].Should().BeEquivalentTo(new[] { 1, 3, 5, 7 });
        }

        private static DirectedGraph<int> GetTestGraph()
        {
            var graph = new DirectedGraph<int>();
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 7);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 1);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 0);
            graph.AddEdge(7, 4);
            graph.AddEdge(7, 5);
            graph.AddEdge(8, 2);
            graph.AddEdge(8, 6);
            return graph;
        }
    }
}
