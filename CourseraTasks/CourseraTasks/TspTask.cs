using System;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class TspTask : ITask
    {
        public void Run()
        {
            var coordinates = new[,]
                {
                    { 20833.3333, 17100.0000 }, { 20900.0000, 17066.6667 }, { 21300.0000, 13016.6667 }, { 21600.0000, 14150.0000 }, { 21600.0000, 14966.6667 },
                    { 21600.0000, 16500.0000 }, { 22183.3333, 13133.3333 }, { 22583.3333, 14300.0000 }, { 22683.3333, 12716.6667 }, { 23616.6667, 15866.6667 },
                    { 23700.0000, 15933.3333 }, { 23883.3333, 14533.3333 }, { 24166.6667, 13250.0000 }, { 25149.1667, 12365.8333 }, { 26133.3333, 14500.0000 },
                    { 26150.0000, 10550.0000 }, { 26283.3333, 12766.6667 }, { 26433.3333, 13433.3333 }, { 26550.0000, 13850.0000 }, { 26733.3333, 11683.3333 },
                    { 27026.1111, 13051.9444 }, { 27096.1111, 13415.8333 }, { 27153.6111, 13203.3333 }, { 27166.6667, 9833.3333 },  { 27233.3333, 10450.0000 }
                };

            var n = 25;

            var distances = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else
                    {
                        var distance = CalculateDistance(coordinates[i, 0], coordinates[i, 1], coordinates[j, 0], coordinates[j, 1]);
                        distances[i, j] = distance;
                        distances[j, i] = distance;
                    }
                }

            var tsp = new Tsp(n, distances);
            var length = tsp.GetShortestRouteLength();
            Console.WriteLine(length);
        }

        private double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            var deltaX = x1 - x2;
            var deltaY = y1 - y2;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
