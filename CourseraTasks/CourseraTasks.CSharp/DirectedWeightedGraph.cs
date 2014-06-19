using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseraTasks.CSharp
{
    public class DirectedWeightedGraph
    {
        private readonly List<Node> _nodes;

        public DirectedWeightedGraph()
        {
            _nodes = new List<Node>();
        }

        public int NodesCount
        {
            get
            {
                return _nodes.Count;
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            var node = GetOrCreateNode(from);
            GetOrCreateNode(to);
            node.AddEdge(to, weight);
        }

        public IEnumerable<WeightedEdge> GetEdges(int node)
        {
            return _nodes[node].Edges;
        }

        private Node GetOrCreateNode(int nodeIndex)
        {
            var missedNodesCount = nodeIndex - _nodes.Count + 1;
            for (int i = 0; i < missedNodesCount; i++)
            {
                _nodes.Add(new Node());
            }

            return _nodes[nodeIndex];
        }

        private class Node
        {
            private readonly List<WeightedEdge> _edges;

            public Node()
            {
                _edges = new List<WeightedEdge>();
            }
            
            public IEnumerable<WeightedEdge> Edges 
            { 
                get 
                {
                    return new ReadOnlyCollection<WeightedEdge>(_edges);
                }
            }

            public void AddEdge(int to, int weight)
            {
                _edges.Add(new WeightedEdge(to, weight));
            }
        }
    }
}
