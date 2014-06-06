using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
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
