using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class Node
    {
        public Node()
        {
            OutEdges = new List<int>();
            InEdges = new List<int>();
        }

        public ICollection<int> OutEdges { get; private set; }

        public ICollection<int> InEdges { get; private set; }
    }
}
