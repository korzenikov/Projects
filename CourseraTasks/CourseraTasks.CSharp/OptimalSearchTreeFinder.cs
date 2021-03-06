﻿using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class OptimalSearchTreeFinder
    {
        public static double GetMinAverageSearchTime(IReadOnlyList<double> weights)
        {
            int n = weights.Count;
            var solution = GetSolutions(weights);
            return solution[0, n - 1];
        }

        public static TreeNode<int> GetTree(IReadOnlyList<double> weights)
        {
            int n = weights.Count;
            var solution = GetSolutions(weights);

            return GetTree(solution, 0, n - 1);
        }

        private static TreeNode<int> GetTree(double[,] solutions, int i, int j)
        {
            if (i > j)
            {
                return null;
            }

            if (i == j)
            {
                return new TreeNode<int>(i, null, null);
            }

            double min = double.MaxValue;
            int rootIndex = -1;
            for (int r = i; r <= j; r++)
            {
                double time;
                if (i > r - 1)
                    time = solutions[r + 1, j];
                else if (r + 1 > j)
                    time = solutions[i, r - 1];
                else
                    time = solutions[i, r - 1] + solutions[r + 1, j];
                if (time < min)
                {
                    min = time;
                    rootIndex = r;
                }
            }

            var leftChild = GetTree(solutions, i, rootIndex - 1);
            var rightChild = GetTree(solutions, rootIndex + 1, j);

            return new TreeNode<int>(rootIndex, leftChild, rightChild);
        }

        private static double[,] GetSolutions(IReadOnlyList<double> weights)
        {
            int n = weights.Count;
            var optimalSolution = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                optimalSolution[i, i] = weights[i];
            }

            for (int s = 1; s < n; s++)
                for (int i = 0; i < n - s; i++)
                {
                    int j = i + s;
                    double p = Enumerable.Range(i, s + 1).Select(x => weights[x]).Sum();
                    double min = Enumerable.Range(i, s + 1).Select(x =>
                    {
                        if (i > x - 1)
                            return optimalSolution[x + 1, j];
                        if (x + 1 > j)
                            return optimalSolution[i, x - 1];
                        return optimalSolution[i, x - 1] + optimalSolution[x + 1, j];
                    }).Min();
                    optimalSolution[i, j] = p + min;
                }

            return optimalSolution;
        }
    }
}
