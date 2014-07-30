using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class JohnsonTask : ITask
    {
        public void Run()
        {
            using (var writer = new StreamWriter("output.txt"))
            {
                //int? distance1, distance2, distance3;

                //using (var reader = new StreamReader("InputFiles//g1.txt"))
                //{
                //    var graph = DirectedWeightedGraphReader.GetGraph(reader, false);
                //    distance1 = GetShortestDistance(Johnson.GetShortestPaths(graph));
                //}

                //using (var reader = new StreamReader("InputFiles//g2.txt"))
                //{
                //    var graph = DirectedWeightedGraphReader.GetGraph(reader, false);
                //    distance2 = GetShortestDistance(Johnson.GetShortestPaths(graph));
                //}

                //using (var reader = new StreamReader("InputFiles//g3.txt"))
                //{
                //    var graph = DirectedWeightedGraphReader.GetGraph(reader, false);
                //    distance3 = GetShortestDistance(Johnson.GetShortestPaths(graph));
                //}

                //writer.WriteLine(
                //    Math.Min(
                //        Math.Min(distance1.GetValueOrDefault(int.MaxValue), distance2.GetValueOrDefault(int.MaxValue)),
                //        distance3.GetValueOrDefault(int.MaxValue)));

                int? distance;

                using (var reader = new StreamReader("InputFiles//large.txt"))
                {
                    var graph = DirectedWeightedGraphReader.GetGraph(reader, false);
                    distance = GetShortestDistance(Johnson.GetShortestPaths(graph));
                }

                writer.WriteLine(distance);
            }
        }

        private static int? GetShortestDistance(int?[,] distances)
        {
            if (distances == null) return null;
            int min = int.MaxValue;

            foreach (var distance in distances)
            {
                if (distance == null)
                    continue;
                if (distance.Value < min)
                {
                    min = distance.Value;
                }
            }

            return min;
        }
    }
}