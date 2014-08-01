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
            using (var reader = new StreamReader("InputFiles//tsp.txt"))
            using (var writer = new StreamWriter("output.txt"))
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

                var heldKarp = new HeldKarp2(n, distances);

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
