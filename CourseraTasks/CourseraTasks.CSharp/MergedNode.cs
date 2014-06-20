using System.Collections.Immutable;

namespace CourseraTasks.CSharp
{
    public class MergedNode
    {
        public MergedNode(int node)
        {
            Nodes = ImmutableHashSet.Create(node);
        }

        public MergedNode(ImmutableHashSet<int> nodes)
        {
            Nodes = nodes;
        }

        public ImmutableHashSet<int> Nodes { get; private set; }

        public MergedNode Merge(MergedNode node)
        {
            return new MergedNode(Nodes.Union(node.Nodes));
        }
    }
}