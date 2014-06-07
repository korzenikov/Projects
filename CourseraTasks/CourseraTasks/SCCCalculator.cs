using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class SCCCalculator
    {
        private ISet<int> _exploredNodes;
        private Node[] _adjacencyList;

        public SCCCalculator(Node[] adjacencyList)
        {
            _adjacencyList = adjacencyList;
            _exploredNodes = new HashSet<int>();
        }

        public IEnumerable<int[]> GetSCCs()
        {
            var nodes = DepthFirstSeachLoop(Enumerable.Range(0, _adjacencyList.Length), true).SelectMany(x => x).Reverse().ToArray();
            return DepthFirstSeachLoop(nodes, false);
        }

        public IEnumerable<int[]> DepthFirstSeachLoop(IEnumerable<int> nodes, bool reverse)
        {
            _exploredNodes = new HashSet<int>();
            foreach (var node in nodes)
            {
                if (!_exploredNodes.Contains(node))
                {
                    var result = DepthFirstSeach(node, reverse).ToArray();
                    yield return result;
                }
            }
        }

        public IEnumerable<int> DepthFirstSeach(int startNode, bool reversed)
        {
            var nodesToVisit = new Stack<int>();
            nodesToVisit.Push(startNode);
            _exploredNodes.Add(startNode);
            while (nodesToVisit.Count != 0)
            {
                var node = nodesToVisit.Peek();

                var adjacentNodes = reversed ? _adjacencyList[node].InEdges : _adjacencyList[node].OutEdges;
                var unexploredAdjacentNodes = adjacentNodes.Where(x => !_exploredNodes.Contains(x)).ToArray();

                if (unexploredAdjacentNodes.Length == 0)
                {
                    nodesToVisit.Pop();
                    yield return node;
                }
                else
                {
                    foreach (var adjancentNode in unexploredAdjacentNodes)
                    {
                        _exploredNodes.Add(adjancentNode);
                        nodesToVisit.Push(adjancentNode);
                    }
                }
            }
        }

    }
}
