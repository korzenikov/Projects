﻿using System;
using System.Diagnostics;

namespace CourseraTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITask task = new KnapsackSolverTask();
            Stopwatch sw = Stopwatch.StartNew();
            task.Run();
            sw.Stop();
            Console.WriteLine("Elapsed milliseconds: {0}", sw.ElapsedMilliseconds);
        }
    }
}
