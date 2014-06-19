using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class MergedNode
    {
        public MergedNode(IEnumerable<int> nodes)
        {
            Nodes = nodes;
        }

        public MergedNode(int node)
        {
            Nodes = new List<int> { node };
        }

        public IEnumerable<int> Nodes { get; private set; }

        public MergedNode Merge(MergedNode node)
        {
            var nodes = Nodes.ToList();
            nodes.AddRange(node.Nodes);
            return new MergedNode(nodes);
        }
    }
}