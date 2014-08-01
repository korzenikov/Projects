using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class HeldKarp
    {
        private readonly int _verticesCount;

        private readonly float[,] _distances;

       public HeldKarp(int verticesCount, float[,] distances)
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


            var calculatedDistances = Enumerable.Repeat(float.PositiveInfinity, _verticesCount).ToArray();

            calculatedDistances[0] = 0;

            var subsets = new List<int> { 1 };

            var currentDistances = new Dictionary<int, float[]> { { 1, calculatedDistances } };

            for (int m = 0; m < _verticesCount - 1; m++)
            {
                var newSubsets = new List<int>();
                foreach (var subset in subsets)
                {
                    for (int v = 1; v < _verticesCount; v++)
                        if (!BitHelper.IsBitSet(subset, v))
                        {
                            var newSubset = BitHelper.SetBit(subset, v);
                            if (!currentDistances.ContainsKey(newSubset))
                            {
                                currentDistances.Add(newSubset, Enumerable.Repeat(float.PositiveInfinity, _verticesCount).ToArray());
                                newSubsets.Add(newSubset);
                            }

                            currentDistances[newSubset][v] =
                                Enumerable.Range(0, _verticesCount)
                                    .Where(k => BitHelper.IsBitSet(subset, k))
                                    .Select(k => currentDistances[subset][k] + _distances[k, v])
                                    .Min();
                        }

                    currentDistances.Remove(subset);
                }
                
                subsets = newSubsets;
            }

            return Enumerable.Range(1, _verticesCount - 1).Select(v => currentDistances[visitedVertices][v] + _distances[v, 0]).Min();
        }
    }
}
