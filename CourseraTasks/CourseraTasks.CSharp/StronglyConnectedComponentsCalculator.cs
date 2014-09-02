using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class StronglyConnectedComponentsCalculator<T>
    {
        private readonly DirectedGraph<T> _graph;

        private readonly ISet<T> _exploredNodes;

        public StronglyConnectedComponentsCalculator(DirectedGraph<T> graph)
        {
            _graph = graph;
            _exploredNodes = new HashSet<T>();
        }

        public IEnumerable<T[]> GetStronglyConnectedComponents()
        {
            var nodes = DepthFirstSearchLoop(_graph.Keys, true).SelectMany(x => x).Reverse().ToArray();
            return DepthFirstSearchLoop(nodes, false);
        }

        public IEnumerable<T[]> DepthFirstSearchLoop(IEnumerable<T> nodes, bool reverse)
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

        public IEnumerable<T> DepthFirstSearch(T startNode, bool reversed)
        {
            var nodesToVisit = new Stack<T>();
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
