using System;

namespace WaterMeasurement
{
    class Program
    {
        public static int MeasureWater(int[] buildingHeights)
        {
            var water = 0;

            var length = buildingHeights.Length;
            var leftIndex = 0;
            var rightIndex = length - 1;
            var leftMaximum = buildingHeights[leftIndex];
            var rightMaximum = buildingHeights[rightIndex];
            while (leftIndex <= rightIndex)
            {
                // Move from left to right
                if (leftMaximum <= rightMaximum)
                {
                    var height = buildingHeights[leftIndex];
                    if (height > leftMaximum)
                        leftMaximum = height;
                    else
                        water += leftMaximum - height;
                    leftIndex++;
                }
                else
                {
                    var height = buildingHeights[rightIndex];
                    if (height > rightMaximum)
                        rightMaximum = height;
                    else
                    {
                        water += rightMaximum - height;
                    }
                    rightIndex--;
                }
            }

            return water;
        }

        public static int MeasureWater2(int[] data)
        {
            int a = 0, z, sum = 0;
            while (a < data.Length - 1)
            {
                for (z = a + 1; z < data.Length - 1 && data[z] < data[a]; z++);
                int max = Math.Min(data[a], data[z]);
                for (a = a + 1; a < z; a++) sum += Math.Max(0, max - data[a]);
            }


            return sum;
        }

        static void Main(string[] args)
        {
            var buildingHeights1 = new[] { 5, 4, 3, 2, 1, 2, 3, 4};
            var buildingHeights2 = new[] { 2, 1, 4, 3, 4, 1, 2 };
            var buildingHeights3 = new[] { 2, 5, 1, 2, 3, 4, 7, 7, 6 };
            var buildingHeights4 = new[] { 5, 7, 5, 4, 9, 8, 6, 7, 4, 9, 6, 4 };
            var buildingHeights5 = new[] { 1, 3, 5, 6, 4, 2 };
            var buildingHeights6 = new[] { 3, 1, 2 };
            var buildingHeights7 = new[] { 9, 8, 6, 7, 4 };


            var tests = new[] { buildingHeights1, buildingHeights2, buildingHeights3, buildingHeights4, buildingHeights5, buildingHeights6, buildingHeights7 };
            for (int i = 0; i < tests.Length; i++)
            {
                Console.WriteLine("Test C#  {0}: {1}", i + 1, MeasureWater(tests[i]));
                Console.WriteLine("Test C#2 {0}: {1}", i + 1, MeasureWater2(tests[i]));
                Console.WriteLine("Test F#  {0}: {1}", i + 1, WMFLIb.WaterMeasurement.measureWater(tests[i]));
            }
        }
    }
}
