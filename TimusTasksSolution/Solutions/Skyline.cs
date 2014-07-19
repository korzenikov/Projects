using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions
{
    public class Skyline
    {
        private void Run()
        {
            //TextReader inputReader = Console.In;
            //string input = inputReader.ReadLine();
            IEnumerable<Building> buildings = GetTestBuildings();
            IEnumerable<Point> skyline = GetSkyLine(buildings);
            IEnumerable<int> numbers = skyline.SelectMany(point => new[] { point.X, point.Y });
            string output = string.Join(" ", numbers);
            Console.WriteLine(output);
        }

        private IEnumerable<Point> GetSkyLine(IEnumerable<Building> buildings)
        {
            var buildingsArray = buildings.ToArray();
            var leftPoints = new Queue<Point>(buildingsArray.Select(item => new Point(item.Left, item.Height)));
            var rightPoints = new Queue<Point>(buildingsArray.Select(item => new Point(item.Right, item.Height)).OrderBy(item => item.X));
            var heights = new MaxHeap<int>(int.MinValue);
            heights.Insert(0);
            int height = 0;
            while (leftPoints.Count != 0 || rightPoints.Count != 0)
            {
                bool takeLeftPoint = leftPoints.Count != 0 && leftPoints.Peek().X < rightPoints.Peek().X;

                int x;
                if (takeLeftPoint)
                {
                    Point point = leftPoints.Dequeue();
                    x = point.X;
                    heights.Insert(point.Y);
                }
                else
                {
                    Point point = rightPoints.Dequeue();
                    x = point.X;
                    heights.Remove(point.Y);
                }

                int maxHeight = heights.Maximum();
                if (maxHeight != height)
                {
                    height = maxHeight;
                    yield return new Point(x, height);
                }
            }
        }

        private IEnumerable<Building> GetTestBuildings()
        {
            return new[] 
            { 
                new Building(1, 11, 5),
                new Building(2, 6, 7),
                new Building(3, 13, 9),
                new Building(12, 7, 16),
                new Building(14, 3, 25),
                new Building(19, 18, 22),
                new Building(23, 13, 29),
                new Building(24, 4, 28)
            };
        }

        public struct Building
        {
            public int Left;
            public int Right;
            public int Height;

            public Building(int left, int height, int right)
            {
                Left = left;
                Right = right;
                Height = height;
            }
        }

        public struct Point
        {
            public int X;
            public int Y;

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
