using System;

namespace CourseraTasks.CSharp
{
    public class QuickSort
    {
        public static void Sort(int[] array, Func<int[],int,int,int> pivotSelector)
        {
            SortInternal(array, 0, array.Length - 1, pivotSelector);
        }

        public static int SortAndCount(int[] array, Func<int[], int, int, int> pivotSelector)
        {
            return SortAndCountInternal(array, 0, array.Length - 1, pivotSelector);
        }

        public static int Partition(int[] array, int l, int r, Func<int[], int, int, int> pivotSelector)
        {
            var pivot = pivotSelector(array, l, r);
            Swap(array, pivot, l);
            int j = l + 1;
            for (int i = l + 1; i <= r; i++)
            {
                if (array[i] < array[l])
                {
                    Swap(array, i, j);
                    j++;
                }
            }

            Swap(array, j - 1, l);

            return j - 1;
        }

        public static void SortInternal(int[] array, int l, int r, Func<int[],int,int,int> pivotSelector)
        {
            if (r - l < 0)
            {
                return;
            }

            var pivot = Partition(array, l, r, pivotSelector);
            SortInternal(array, l, pivot - 1, pivotSelector);
            SortInternal(array, pivot + 1, r, pivotSelector);
        }

        public static int SortAndCountInternal(int[] array, int l, int r, Func<int[],int,int,int> pivotSelector)
        {
            if (r - l < 0)
            {
                return 0;
            }

            var pivot = Partition(array, l, r, pivotSelector);
            return r - l + SortAndCountInternal(array, l, pivot - 1, pivotSelector) + SortAndCountInternal(array, pivot + 1, r, pivotSelector);
        }

        public static int GetMedian(int[] array, int l, int r)
        {
            int mid = l + (r - l + 1) / 2 - 1 + (r - l + 1) % 2;
            if (array[l] < array[mid])
            {
               if (array[mid] < array[r])
                   return mid;
                return array[l] < array[r] ? r : l;
            }

            if (array[l] < array[r])
                return l;
            return array[mid] < array[r] ? r : mid;
        }

        private static void Swap(int[] array, int i, int j)
        {
            if (i != j)
            {
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
