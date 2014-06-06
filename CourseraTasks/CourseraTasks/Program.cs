using System;
using System.IO;

namespace CourseraTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var reader = new StreamReader("SCC.txt"))
            using (var writer = new StreamWriter("output.txt"))
            //using (var writer = Console.Out)
            {
                var task = new SCCCalculatorTask();
                task.Run(reader, writer);
            }
        }
    }
}
