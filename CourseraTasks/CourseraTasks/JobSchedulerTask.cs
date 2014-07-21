using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class JobSchedulerTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("jobs.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var jobs = new List<Job>();
                reader.ReadLine();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var numbers = parts.Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();
                    jobs.Add(new Job(numbers[0], numbers[1]));
                }

                var weightedSum1 = JobScheduler.GetWeightedSumOfCompletionTimes(jobs, JobScheduler.CompareByDifference);
                writer.WriteLine(weightedSum1);

                var weightedSum2 = JobScheduler.GetWeightedSumOfCompletionTimes(jobs, JobScheduler.CompareByRatio);
                writer.WriteLine(weightedSum2);
            }
        }
    }
}
