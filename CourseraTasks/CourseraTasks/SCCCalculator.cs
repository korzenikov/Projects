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
            var discoveredNodes = new HashSet<int>();
            nodesToVisit.Push(startNode);
            while (nodesToVisit.Count != 0)
            {
                var node = nodesToVisit.Peek();
                if (exploredNodes.Contains(node))
                {
                    nodesToVisit.Pop();
                    yield return node;
                }
                else
                {
                    exploredNodes.Add(node);
                    var nodes = reversed ? adjacencyList[node].InEdges : adjacencyList[node].OutEdges;
                    var unexploredNodes = nodes.Where(x => !exploredNodes.Contains(x) && !discoveredNodes.Contains(x)).ToArray();

                    foreach (var adjancentNode in unexploredNodes)
                    {
                        discoveredNodes.Add(adjancentNode);
                        nodesToVisit.Push(adjancentNode);
                    }
                }
            }
        }

    }
}
