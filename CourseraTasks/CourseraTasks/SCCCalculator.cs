using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class SCCCalculator
    {
        public static IEnumerable<int[]> GetSCCs(ICollection<int>[] adjacencyList)
        {
            var nodes = DepthFirstSeachLoop(adjacencyList, Enumerable.Range(0, adjacencyList.Length), true).SelectMany(x => x).Reverse().ToArray();
            return DepthFirstSeachLoop(adjacencyList, nodes, false);
        }

        public static IEnumerable<int[]> DepthFirstSeachLoop(ICollection<int>[] adjacencyList, IEnumerable<int> nodes, bool reverse)
        {
            var exploredNodes = new HashSet<int>();
            foreach (var node in nodes)
            {
                if (!exploredNodes.Contains(node))
                {
                    yield return DepthFirstSeach(adjacencyList, exploredNodes, node, reverse).ToArray();
                }
            }
        }

        public static IEnumerable<int> DepthFirstSeach(ICollection<int>[] adjacencyList, ISet<int> exploredNodes, int startNode, bool reversed)
        {
            var nodesToVisit = new Stack<int>();
            nodesToVisit.Push(startNode);
            while (nodesToVisit.Count != 0)
            {
                var node = nodesToVisit.Peek();
                if (exploredNodes.Contains(node))
                {
                    nodesToVisit.Pop();
                    yield return node;
                    continue;
                }

                exploredNodes.Add(node);
                var unexploredNodes = (reversed ? Enumerable.Range(0, adjacencyList.Length).Where(x => adjacencyList[x].Contains(node)) : adjacencyList[node]).Where(x => !exploredNodes.Contains(x)).ToArray();
                
                foreach (var adjancentNode in unexploredNodes)
                {
                    nodesToVisit.Push(adjancentNode);
                }
            }
        }

    }
}
