using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class MedianMaintenanceTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("Median.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var numbers = SequenceOfNumbersReader.GetNumbers(reader);
                var sum = MedianMaintenance.GetMedians(numbers).Sum() % 10000;
                writer.WriteLine(sum);
            }
        }
    }
}
