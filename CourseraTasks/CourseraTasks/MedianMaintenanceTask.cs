﻿using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class MedianMaintenanceTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("Median.txt"))
            //using (var writer = Console.Out)
            using (var writer = new StreamWriter("output.txt"))
            {
                var numbers = GetNumbers(reader);
                var sum = MedianMaintenance.GetMedians(numbers).Sum() % 10000;
                writer.WriteLine(sum);
            }
        }

        private static IEnumerable<int> GetNumbers(TextReader inputReader)
        {
            while (true)
            {
                string numberStr = inputReader.ReadLine();
                if (numberStr == null)
                {
                    break;
                }

                yield return int.Parse(numberStr);
            }
        }
    }
}