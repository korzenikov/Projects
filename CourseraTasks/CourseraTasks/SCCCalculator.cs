using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class SCCCalculator
    {
        public static IEnumerable<int[]> GetSCCs(Node[] adjacencyList)
        {
            var nodes = DepthFirstSeachLoop(adjacencyList, Enumerable.Range(0, adjacencyList.Length), true).SelectMany(x => x).Reverse().ToArray();
            return DepthFirstSeachLoop(adjacencyList, nodes, false);
        }

        public static IEnumerable<int[]> DepthFirstSeachLoop(Node[] adjacencyList, IEnumerable<int> nodes, bool reverse)
        {
            var exploredNodes = new HashSet<int>();
            foreach (var node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {
                    var result = DepthFirstSeach(adjacencyList, exploredNodes, node, reverse).ToArray();
                    yield return result;
                }
            }
        }

        public static IEnumerable<int> DepthFirstSeach(Node[] adjacencyList, ISet<int> exploredNodes, int startNode, bool reversed)
        {
            var nodesToVisit = new Stack<int>();
            nodesToVisit.Push(startNode);
            exploredNodes.Add(startNode);
            while (nodesToVisit.Count != 0)
            {
                var node = nodesToVisit.Peek();

                var adjacentNodes = reversed ? adjacencyList[node].InEdges : adjacencyList[node].OutEdges;
                var unexploredAdjacentNodes = adjacentNodes.Where(x => !exploredNodes.Contains(x)).ToArray();

                if (unexploredAdjacentNodes.Length == 0)
                {
                    nodesToVisit.Pop();
                    yield return node;
                }
                else
                {
                    foreach (var adjancentNode in unexploredAdjacentNodes)
                    {
                        exploredNodes.Add(adjancentNode);
                        nodesToVisit.Push(adjancentNode);
                    }
                }
            }
        }

    }
}
