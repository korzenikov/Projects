using System.Collections.Generic;
using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class SccCalculatorTest
    {
        [TestMethod]
        public void DepthFirstSearchTest()
        {
            var adjacencyList = new[]
                {
                    new[] { 3 }, new[] { 7 }, new[] { 5 }, new[] { 6 }, new[] { 1 }, new[] { 8 }, new[] { 0 }, new[] { 4, 5 }, new[] { 2, 6 } 
                };
            var nodes = GetNodes(adjacencyList);

            var calculator1 = new SccCalculator(nodes);
            IEnumerable<int> vertices1 = calculator1.DepthFirstSearch(0, true);
            vertices1.Should().HaveCount(9);

            var calculator2 = new SccCalculator(nodes);
            IEnumerable<int> vertices2 = calculator2.DepthFirstSearch(1, true);
            vertices2.Should().HaveCount(3);

            var adjacencyList2 = new[] { new[] { 1, 2 }, new int[] { }, new[] { 1 }, };

            var nodes2 = GetNodes(adjacencyList2);
            var calculator3 = new SccCalculator(nodes2);
            IEnumerable<int> vertices3 = calculator3.DepthFirstSearch(0, false);
            vertices3.Should().HaveCount(3);
        }

        [TestMethod]
        public void DepthFirstSeachLoopTest()
        {
            var adjacencyList = new[]
                {
                    new[] { 3 }, new[] { 7 }, new[] { 5 }, new[] { 6 }, new[] { 1 }, new[] { 8 }, new[] { 0 }, new[] { 4, 5 }, new[] { 2, 6 } 
                };
            var nodes = GetNodes(adjacencyList);
            var calculator1 = new SccCalculator(nodes);
            var result1 = calculator1.DepthFirstSeachLoop(Enumerable.Range(0, nodes.Length), true).ToArray();
            result1.Length.Should().BeLessOrEqualTo(3);

            var calculator2 = new SccCalculator(nodes);
            var result2 = calculator2.DepthFirstSeachLoop(Enumerable.Range(0, nodes.Length).Reverse(), true).ToArray();
            result2.Length.Should().BeLessOrEqualTo(3);
        }

        [TestMethod]
        public void GetSccsTest()
        {
            var adjacencyList = new[]
                {
                    new[] { 3 }, new[] { 7 }, new[] { 5 }, new[] { 6 }, new[] { 1 }, new[] { 8 }, new[] { 0 }, new[] { 4, 5 }, new[] { 2, 6 } 
                };
            var nodes = GetNodes(adjacencyList);
            var calculator = new SccCalculator(nodes);
            var components = calculator.GetSccs().ToArray();
            components.Should().HaveCount(3);
            components[0].Should().BeEquivalentTo(new[] { 0, 6, 3 });
            components[1].Should().BeEquivalentTo(new[] { 8, 5, 2 });
            components[2].Should().BeEquivalentTo(new[] { 7, 1, 4 });
        }

        private static Node[] GetNodes(int[][] adjacencyList)
        {            
            var nodes = new Node[adjacencyList.Length];

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                nodes[i] = new Node();
            }

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                foreach (var j in adjacencyList[i])
                {
                    nodes[i].OutEdges.Add(j);
                    nodes[j].InEdges.Add(i);
                }
            }

            return nodes;
        }
    }
}
