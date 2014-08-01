using System;
using System.Globalization;
using System.IO;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class HeldKarpTask : ITask
    {
        public void Run()
        {
            //var coordinates = new[,]
            //    {
            //        { 20833.3333f, 17100.0000f }, { 20900.0000f, 17066.6667f }, { 21300.0000f, 13016.6667f }, { 21600.0000f, 14150.0000f }, { 21600.0000f, 14966.6667f },
            //        { 21600.0000f, 16500.0000f }, { 22183.3333f, 13133.3333f }, { 22583.3333f, 14300.0000f }, { 22683.3333f, 12716.6667f }, { 23616.6667f, 15866.6667f },
            //        { 23700.0000f, 15933.3333f }, { 23883.3333f, 14533.3333f }, { 24166.6667f, 13250.0000f }, { 25149.1667f, 12365.8333f }, { 26133.3333f, 14500.0000f },
            //        { 26150.0000f, 10550.0000f }, { 26283.3333f, 12766.6667f }, { 26433.3333f, 13433.3333f }, { 26550.0000f, 13850.0000f }, { 26733.3333f, 11683.3333f },
            //        { 27026.1111f, 13051.9444f }, { 27096.1111f, 13415.8333f }, { 27153.6111f, 13203.3333f }, { 27166.6667f, 9833.3333f },  { 27233.3333f, 10450.0000f }
            //    };

            //var n = 15;

            using (var reader = new StreamReader("InputFiles//tsp.txt"))
            using (var writer = new StreamWriter("output_tsp.txt"))
            {
                string row = reader.ReadLine();
                int n = int.Parse(row);
                var coordinates = new float[n, 2];

                for (int i = 0; i < n; i++)
                {
                    row = reader.ReadLine();
                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var x = float.Parse(parts[0], CultureInfo.InvariantCulture);
                    var y = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    coordinates[i, 0] = x;
                    coordinates[i, 1] = y;
                }

                var distances = new float[n, n];
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

                var heldKarp = new HeldKarp(n, distances);

                var length = heldKarp.GetShortestRouteLength();

                writer.WriteLine(length);
            }
        }

        private float CalculateDistance(float x1, float y1, float x2, float y2)
        {
            var deltaX = x1 - x2;
            var deltaY = y1 - y2;
            return (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
