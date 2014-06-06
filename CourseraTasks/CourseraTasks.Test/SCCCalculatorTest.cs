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
    public class SCCCalculatorTest
    {
        [TestMethod]
        public void DepthFirstSeachTest()
        {
            var adjacencyList = new[] { 
                new[] { 3 }, 
                new[] { 7 }, 
                new[] { 5 }, 
                new[] { 6 },
                new[] { 1 }, 
                new[] { 8 }, 
                new[] { 0}, 
                new[] { 4, 5 },
                new[] { 2, 6 }
            };
            var nodes = GetNodes(adjacencyList);

            IEnumerable<int> vertices1 = SCCCalculator.DepthFirstSeach(nodes, new HashSet<int>(), 0, true);
            vertices1.Should().HaveCount(9);

            IEnumerable<int> vertices2 = SCCCalculator.DepthFirstSeach(nodes, new HashSet<int>(), 1, true);
            vertices2.Should().HaveCount(3);


            var adjacencyList2 = new[] 
            { 
                new[] { 1, 2 }, 
                new int[] {  },
                new[] { 1 }, 
            };

            var nodes2 = GetNodes(adjacencyList2);

            IEnumerable<int> vertices3 = SCCCalculator.DepthFirstSeach(nodes2, new HashSet<int>(), 0, false);
            vertices3.Should().HaveCount(3);
        }

        [TestMethod]
        public void DepthFirstSeachLoopTest()
        {
            var adjacencyList = new[] { 
                new[] { 3 }, 
                new[] { 7 }, 
                new[] { 5 }, 
                new[] { 6 },
                new[] { 1 }, 
                new[] { 8 }, 
                new[] { 0}, 
                new[] { 4, 5 },
                new[] { 2, 6 }
            };
            var nodes = GetNodes(adjacencyList);

            var result1 = SCCCalculator.DepthFirstSeachLoop(nodes, Enumerable.Range(0, nodes.Length), true).ToArray();
            result1.Length.Should().BeLessOrEqualTo(3);

            var result2 = SCCCalculator.DepthFirstSeachLoop(nodes, Enumerable.Range(0, nodes.Length).Reverse(), true).ToArray();
            result2.Length.Should().BeLessOrEqualTo(3);
        }

        [TestMethod]
        public void GetSCCsTest()
        {
            var adjacencyList = new[] { 
                new[] { 3 }, 
                new[] { 7 }, 
                new[] { 5 }, 
                new[] { 6 },
                new[] { 1 }, 
                new[] { 8 }, 
                new[] { 0}, 
                new[] { 4, 5 },
                new[] { 2, 6 }
            };
            var nodes = GetNodes(adjacencyList);
            var components = SCCCalculator.GetSCCs(nodes).ToArray();
            components.Should().HaveCount(3);
            components[0].Should().BeEquivalentTo(new[] { 0, 6, 3 });
            components[1].Should().BeEquivalentTo(new[] { 8, 5, 2 });
            components[2].Should().BeEquivalentTo(new[] { 7, 1, 4 });
        }

        private Node[] GetNodes(int[][] adjacencyList)
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
