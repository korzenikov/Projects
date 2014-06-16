using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class DirectedWeightedGraph
    {
        private Node[] _nodes;

        public DirectedWeightedGraph(int nodesCount)
        {
            _nodes = new Node[nodesCount];
            for (int i = 0; i < nodesCount; i++)
            {
                _nodes[i] = new Node();
            }
        }

        public ReadOnlyCollection<Node> Nodes
        {
            get
            {
                return new ReadOnlyCollection<Node>(_nodes);
            }
        }

        public void AddEdge(int from, int to, int weight)
        {
            _nodes[from].AddEdge(to, weight);
        }

        public class Node
        {
            private  List<WeightedEdge> _edges;

            public Node()
	        {
                _edges = new List<WeightedEdge>();
            }
            
            public ReadOnlyCollection<WeightedEdge> Edges 
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

            public class WeightedEdge
            {
                public WeightedEdge (int node, int weight)
	            {
                    Node = node;
                    Weight = weight;
                }

                public int Node { get; private set; }

                public int Weight { get; private set; }
        
            }
        }
    }
}
