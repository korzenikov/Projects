namespace CourseraTasks.CSharp
{
    public class Edge
    {
        public Edge(int from, int to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public int From { get; private set; }

        public int To { get; private set; }

        public int Weight { get; private set; }
    }
}
