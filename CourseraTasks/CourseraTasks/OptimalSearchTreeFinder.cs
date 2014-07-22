using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public static class OptimalSearchTreeFinder
    {
        public static double GetMinAverageSearchTime(IReadOnlyList<double> weights)
        {
            int n = weights.Count;
            var A = new double[n, n]; 
            for (int i = 0; i < n; i++)
			{
                A[i, i] = weights[i];
			 
			}

            for (int s = 1; s < n; s++)
                for (int i = 0; i < n - s; i++)
			    {
			        int j = i + s;
                    double p = Enumerable.Range(i, s + 1).Select(x => weights[x]).Sum();
                    double min = Enumerable.Range(i, s + 1).Select(x => 
                    {
                        if (i > x - 1)
                            return A[x + 1, j];
                        if (x + 1 > j)
                            return A[i, x - 1];
                        return A[i, x - 1] + A[x + 1, j];
                    }).Min();
                    A[i, j] = p + min;
                }

            var roots = ReconstructSolution(A, 0, n - 1).ToArray();

            return A[0, n - 1];
        }

        private static IEnumerable<int> ReconstructSolution(double[,] A, int i, int j)
        {
            if (i <= j)
            {
                double min = double.MaxValue;
                int rootIndex = -1;
                for (int r = i; r <= j; r++)
                {
                    double time;
                    if (i > r - 1)
                        time = A[r + 1, j];
                    else if (r + 1 > j)
                        time =  A[i, r - 1];
                    else
                        time = A[i, r - 1] + A[r + 1, j];
                    if (time < min)
                    {
                        min = time;
                        rootIndex = r;
                    }
                }
                yield return rootIndex;

                foreach (var index in ReconstructSolution(A, i, rootIndex - 1))
                {
                    yield return index;
                }

                foreach (var index in ReconstructSolution(A, rootIndex + 1, j))
                {
                    yield return index;
                }
            }
        }
    }
}
