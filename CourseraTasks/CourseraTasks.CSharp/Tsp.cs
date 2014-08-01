namespace CourseraTasks.CSharp
{
    public class Tsp
    {
        private readonly int _verticesCount;

        private readonly double[,] _distances;

        private double _minValue;

        public Tsp(int verticesCount, double[,] distances)
        {
            _verticesCount = verticesCount;
            _distances = distances;
            _minValue = double.PositiveInfinity;
        }

        public double GetShortestRouteLength()
        {
            ChooseVertex(0, 0, 0);
            return _minValue;
        }

        private void ChooseVertex(int visitedVertices, int lastPathVertex, double pathLength)
        {
            if (pathLength > _minValue)
            {
                return;
            }

            bool allChosen = true;

            for (int v = 0; v < _verticesCount; v++)
                if (!BitHelper.IsBitSet(visitedVertices, v))
                {
                    allChosen = false;
                    ChooseVertex(BitHelper.SetBit(visitedVertices, v), v, pathLength + _distances[lastPathVertex, v]);
                }

            if (allChosen)
            {
                if (pathLength < _minValue)
                {
                    _minValue = pathLength + _distances[lastPathVertex, 0];
                }
            }
        }
    }
}
