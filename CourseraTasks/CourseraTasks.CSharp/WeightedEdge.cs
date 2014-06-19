namespace CourseraTasks.CSharp
{
    public class WeightedEdge
    {
        public WeightedEdge(int endNode, int weight)
        {
            EndNode = endNode;
            Weight = weight;
        }

        public int EndNode { get; private set; }

        public int Weight { get; private set; }
    }
}