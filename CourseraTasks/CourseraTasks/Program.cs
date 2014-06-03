using System.IO;

namespace CourseraTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var reader = new StreamReader("kargerMinCut.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var task = new MinCutTask();
                task.Run(reader, writer);
            }
        }
    }
}
