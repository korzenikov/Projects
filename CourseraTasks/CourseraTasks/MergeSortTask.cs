using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class MergeSortTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("IntegerArray.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var numbers = SequenceOfNumbersReader.GetNumbers(reader).ToArray();
                var result = MergeSort.CountInversions(numbers);
                writer.WriteLine(result);
            }
        }
    }
}
