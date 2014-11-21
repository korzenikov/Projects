using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class BubbleSort
    {
        public static IEnumerable<int> Sort(IEnumerable<int> sequence)
        {
            int iterationCount = 0;
            var array = sequence.ToArray();
            int length = array.Length;
            while (length > 1)
            {
                bool hasSwap = false;
                for (int i = 1; i < length; i++)
                {
                    iterationCount++;
                    if (array[i - 1] > array[i])
                    {
                        int temp = array[i - 1];
                        array[i - 1] = array[i];
                        array[i] = temp;
                        hasSwap = true;
                    }
                }

                length--;

                if (!hasSwap)
                {
                    break;
                }
            }

            Debug.WriteLine(iterationCount);
            
            return array;
        }
    }
}
