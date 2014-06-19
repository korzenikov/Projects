using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseraTasks.CSharp
{
    public class DirectedGraph
    {
        private readonly List<Node> _nodes;

        public DirectedGraph()
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

        public void AddEdge(int from, int to)
        {
            Node startNode = GetOrCreateNode(from);
            Node endNode = GetOrCreateNode(to);
            startNode.AddOutEdge(to);
            endNode.AddInEdge(from);
        }

        public IEnumerable<int> GetInNodes(int node)
        {
            return _nodes[node].InNodes;
        }

        public IEnumerable<int> GetOutNodes(int node)
        {
            return _nodes[node].OutNodes;
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
            private readonly List<int> _outNodes;

            private readonly List<int> _inNodes;

            public Node()
            {
                _outNodes = new List<int>();
                _inNodes = new List<int>();
            }

            public IEnumerable<int> OutNodes
            {
                get
                {
                    return new ReadOnlyCollection<int>(_outNodes);
                }
            }

            public IEnumerable<int> InNodes
            {
                get
                {
                    return new ReadOnlyCollection<int>(_inNodes);
                }
            }

            public void AddOutEdge(int node)
            {
                _outNodes.Add(node);
            }

            public void AddInEdge(int node)
            {
                _inNodes.Add(node);
            }
        }
    }
}
