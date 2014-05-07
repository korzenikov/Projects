using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks
{
    public static class MergeSort
    {
        public static IEnumerable<int> Sort(IEnumerable<int> sequence)
        {
            var array = sequence.ToArray();
            Sort(array, 0, array.Length - 1);
            return array;
        }

        public static long CountInversions(IEnumerable<int> sequence)
        {
            var array = sequence.ToArray();
            return SortAndCountInversions(array, 0, array.Length - 1);
        }

        private static void Sort(int[] array, int l, int r)
        {
            int n = r - l + 1;
            if (n == 1)
            {
                return;
            }

            int p = n / 2;
            Sort(array, l, l + p - 1);
            Sort(array, l + p, r);
            Merge(array, l, r, l + p - 1);
        }

        private static void Merge(int[] array, int l, int r, int m)
        {
            int n = r - l + 1;
            var result = new int[n];
            int i = l;
            int j = m + 1;
            for (int t = 0; t < n; t++)
            {
                if (i <= m && (j > r || array[i] <= array[j]))
                {
                    result[t] = array[i];
                    i++;
                }
                else
                {
                    result[t] = array[j];
                    j++;
                }
            }

            for (int t = 0; t < n; t++)
            {
                array[l + t] = result[t];
            }
        }

        private static long SortAndCountInversions(int[] array, int l, int r)
        {
            int n = r - l + 1;
            if (n == 1)
            {
                return 0;
            }

            int p = n / 2;
            var leftInversions = SortAndCountInversions(array, l, l + p - 1);
            var rightInversions = SortAndCountInversions(array, l + p, r);
            var splitInversions = MergeAndCountInversions(array, l, r, l + p - 1);
            return leftInversions + rightInversions + splitInversions;
        }

        private static long MergeAndCountInversions(int[] array, int l, int r, int m)
        {
            int n = r - l + 1;
            var result = new int[n];
            int i = l;
            int j = m + 1;
            long inversions = 0;
            for (int t = 0; t < n; t++)
            {
                if (i <= m && (j > r || array[i] <= array[j]))
                {
                    result[t] = array[i];
                    i++;
                }
                else
                {
                    result[t] = array[j];
                    j++;
                    inversions += m - i + 1; 
                }
            }

            for (int t = 0; t < n; t++)
            {
                array[l + t] = result[t];
            }

            return inversions;
        }
    }
}
