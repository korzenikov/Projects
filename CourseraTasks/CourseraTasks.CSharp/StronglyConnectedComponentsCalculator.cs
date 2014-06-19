using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class StronglyConnectedComponentsCalculator
    {
        private readonly DirectedGraph _graph;

        private readonly ISet<int> _exploredNodes;

        public StronglyConnectedComponentsCalculator(DirectedGraph graph)
        {
            _graph = graph;
            _exploredNodes = new HashSet<int>();
        }

        public IEnumerable<int[]> GetStronglyConnectedComponents()
        {
            var nodes = DepthFirstSearchLoop(Enumerable.Range(0, _graph.NodesCount), true).SelectMany(x => x).Reverse().ToArray();
            return DepthFirstSearchLoop(nodes, false);
        }

        public IEnumerable<int[]> DepthFirstSearchLoop(IEnumerable<int> nodes, bool reverse)
        {
            _exploredNodes.Clear();
            foreach (var node in nodes)
            {
                if (_exploredNodes.Contains(node))
                {
                    continue;
                }

                var result = DepthFirstSearch(node, reverse).ToArray();
                yield return result;
            }
        }

        public IEnumerable<int> DepthFirstSearch(int startNode, bool reversed)
        {
            var nodesToVisit = new Stack<int>();
            nodesToVisit.Push(startNode);
            _exploredNodes.Add(startNode);
            while (nodesToVisit.Count != 0)
            {
                var node = nodesToVisit.Peek();

                var adjacentNodes = reversed ? _graph.GetInNodes(node) : _graph.GetOutNodes(node);
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
