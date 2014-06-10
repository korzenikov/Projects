using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class ShortestPathFinder
    {
        public static IEnumerable<int> Find(IEnumerable<Edge> edges, int source)
        {
            var dist = new Dictionary<int, int>();
            dist[source] = 0;
            var vertices = edges.SelectMany(edge => new[] { edge.From, edge.To }).Distinct().ToArray();
            foreach (var vertex in vertices)
            {
                if (vertex != source)
                {
                    dist[vertex] = int.MaxValue;
                }
            }

            return Enumerable.Empty<int>();
        }
    }
}
