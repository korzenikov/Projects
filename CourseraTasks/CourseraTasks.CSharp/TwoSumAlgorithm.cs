using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class TwoSumAlgorithm
    {
        private readonly HashSet<long> _numbersSet;
        private readonly long[] _numbers;

        public TwoSumAlgorithm(IEnumerable<long> numbers)
        {
            _numbers = numbers.Distinct().ToArray();
            Array.Sort(_numbers);
            _numbersSet = new HashSet<long>(_numbers);
        }

        public bool CanGetSum(long sum)
        {
            var lastIndex = Array.BinarySearch(_numbers, sum);
            if (lastIndex < 0)
            {
                lastIndex = ~lastIndex;
            }

            for (int index = 0; index < lastIndex; index++)
            {
                var number = _numbers[index];
                var complementaryValue = sum - number;
                if (number != complementaryValue && _numbersSet.Contains(complementaryValue))
                {
                    return true;
                }
            }

            return false;
        }

        public int CheckSequence(long from, long to)
        {
            int count = 0;
            int lastIndex = 0;
            for (long sum = from; sum < to; sum++)
            {
                lastIndex = Array.BinarySearch(_numbers, lastIndex, _numbers.Length - lastIndex, sum);
                if (lastIndex < 0)
                {
                    lastIndex = ~lastIndex;
                }

                for (int index = 0; index < lastIndex; index++)
                {
                    var number = _numbers[index];
                    var complementaryValue = sum - number;
                    if (number != complementaryValue && _numbersSet.Contains(complementaryValue))
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }
    }
}   
