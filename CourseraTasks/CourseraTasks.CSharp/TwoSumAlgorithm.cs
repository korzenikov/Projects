using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class TwoSumAlgorithm
    {
        private HashSet<long> _numbersSet;
        private long[] _numbers;

        public TwoSumAlgorithm(IEnumerable<long> numbers)
        {
            _numbers = numbers.Distinct().ToArray();
            _numbersSet = new HashSet<long>(_numbers);
        }

        public bool CheckSum(long sum)
        {
            foreach (var number in _numbers)
            {
                var complementaryValue = sum -  number;
                if (number != complementaryValue && _numbersSet.Contains(complementaryValue))
                {
                    return true;
                }
            }

            return false;
        }
    }
}   
