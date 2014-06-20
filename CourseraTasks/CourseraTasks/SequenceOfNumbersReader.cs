using System.Collections.Generic;
using System.IO;

namespace CourseraTasks
{
    public static class SequenceOfNumbersReader
    {
        public static IEnumerable<int> GetNumbers(TextReader reader)
        {
            while (true)
            {
                string numberStr = reader.ReadLine();
                if (numberStr == null)
                {
                    break;
                }

                yield return int.Parse(numberStr);
            }
        }
    }
}
