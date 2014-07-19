using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace SquareRoots
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader inputReader = Console.In;
            
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;
            string[] input = inputReader.ReadToEnd().Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = input.Length - 1; i >= 0; i--)
            {
                double root = Math.Sqrt(double.Parse(input[i], nfi));
                Console.WriteLine(string.Format(nfi, "{0:F4}", root));
            }
        }
    }
}
