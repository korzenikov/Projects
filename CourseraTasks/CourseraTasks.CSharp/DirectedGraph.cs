using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CourseraTasks.CSharp
{
    public class DirectedGraph<T>
    {
        private readonly IDictionary<T, Node> _nodes;

        public DirectedGraph()
        {
            _nodes = new Dictionary<T, Node>();
        }

        public IEnumerable<T> Keys
        {
            get
            {
                return _nodes.Keys;
            }
        }

        public void AddEdge(T from, T to)
        {
            var startNode = GetOrCreateNode(from);
            var endNode = GetOrCreateNode(to);
            startNode.AddOutEdge(to);
            endNode.AddInEdge(from);
        }

        public IEnumerable<T> GetInNodes(T key)
        {
            return _nodes[key].InNodes;
        }

        public IEnumerable<T> GetOutNodes(T key)
        {
            return _nodes[key].OutNodes;
        }

        private Node GetOrCreateNode(T key)
        {
            if (!_nodes.ContainsKey(key))
            {
                _nodes[key] = new Node();
            }

            return _nodes[key];
        }

        private class Node
        {
            private readonly List<T> _outNodes;

            private readonly List<T> _inNodes;

            public Node()
            {
                _outNodes = new List<T>();
                _inNodes = new List<T>();
            }

            public IEnumerable<T> OutNodes
            {
                get
                {
                    return new ReadOnlyCollection<T>(_outNodes);
                }
            }

            public IEnumerable<T> InNodes
            {
                get
                {
                    return new ReadOnlyCollection<T>(_inNodes);
                }
            }

            public void AddOutEdge(T node)
            {
                _outNodes.Add(node);
            }

            public void AddInEdge(T node)
            {
                _inNodes.Add(node);
            }
        }
    }
}
