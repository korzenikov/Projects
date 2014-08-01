using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class HeldKarp2
    {
        private readonly int _verticesCount;

        private readonly float[,] _distances;

        private float[][] _calculatedDistances;

        public HeldKarp2(int verticesCount, float[,] distances)
        {
            _verticesCount = verticesCount;
            _distances = distances;
        }

        public float GetShortestRouteLength()
        {
            int visitedVertices = 0;

            for (int i = 0; i < _verticesCount; i++)
            {
                visitedVertices = BitHelper.SetBit(visitedVertices, i);
            }

            _calculatedDistances = new float[visitedVertices][];

            _calculatedDistances[1] = new float[_verticesCount - 1];

            for (int v = 1; v < _verticesCount; v++)
            {
                _calculatedDistances[1][v - 1] = _distances[0, v];
            }

            var lengths = new List<float>();

            for (int v = 1; v < _verticesCount; v++)
                {
                    var vertices = BitHelper.ClearBit(visitedVertices, v);
                    var length = GetShortestRouteLength(vertices, v) + _distances[v, 0];
                    lengths.Add(length);
                }

            return lengths.Min();
        }

        private float GetShortestRouteLength(int visitedVertices, int lastPathVertex)
        {
            if (_calculatedDistances[visitedVertices] == null)
            {
                _calculatedDistances[visitedVertices] = Enumerable.Repeat(float.NaN, _verticesCount - 1).ToArray();
            }

            if (double.IsNaN(_calculatedDistances[visitedVertices][lastPathVertex - 1]))
            {
                var lengths = new List<float>();
                for (int v = 1; v < _verticesCount; v++)
                    if (BitHelper.IsBitSet(visitedVertices, v))
                    {
                        var vertices = BitHelper.ClearBit(visitedVertices, v);
                        var length = GetShortestRouteLength(vertices, v) + _distances[v, lastPathVertex];
                        lengths.Add(length);
                    }

                _calculatedDistances[visitedVertices][lastPathVertex - 1] = lengths.Min();
            }

            return _calculatedDistances[visitedVertices][lastPathVertex - 1];
        }
    }
}
