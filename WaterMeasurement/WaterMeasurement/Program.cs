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

        static void Main(string[] args)
        {
            var buildingHeights1 = new[] { 5, 4, 3, 2, 1, 2, 3, 4};
            var buildingHeights2 = new[] { 2, 1, 4, 3, 4, 1, 2 };
            var buildingHeights3 = new[] { 2, 5, 1, 2, 3, 4, 7, 7, 6 };
            var tests = new[] { buildingHeights1, buildingHeights2, buildingHeights3 };
            for (int i = 0; i < tests.Length; i++)
            {
                Console.WriteLine("Test {0}: {1}", i + 1, MeasureWater(tests[i]));
            }
        }
    }
}
