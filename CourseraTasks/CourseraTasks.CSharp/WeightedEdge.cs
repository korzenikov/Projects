namespace CourseraTasks.CSharp
{
    public class WeightedEdge
    {
        public WeightedEdge(int startNode, int endNode, int weight)
        {
            StartNode = startNode;
            EndNode = endNode;
            Weight = weight;
        }

        public int StartNode { get; private set; }

        public int EndNode { get; private set; }

        public int Weight { get; private set; }
    }
}