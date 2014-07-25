using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class JobScheduler
    {
        public static long GetWeightedSumOfCompletionTimes(IEnumerable<Job> jobs, Func<Job, Job, int> comparer)
        {
            var jobArray = jobs.ToArray();
            Array.Sort(jobArray, (job1, job2) => comparer(job1, job2));
            long sum = 0;
            long waitingTime = 0;

            foreach (var job in jobArray)
            {
                sum += (waitingTime + job.Length) * job.Weight;
                waitingTime += job.Length;
            }

            return sum;
        }

        public static int CompareByDifference(Job job1, Job job2)
        {
            var diff = (job2.Weight - job2.Length) - (job1.Weight - job1.Length);
            return diff != 0 ? diff : job2.Weight - job1.Weight;
        }

        public static int CompareByRatio(Job job1, Job job2)
        {
            var ratio1 = job1.Weight / (double)job1.Length;
            var ratio2 = job2.Weight / (double)job2.Length;
            if (ratio1 > ratio2)
            {
                return -1;
            }

            if (ratio1 < ratio2)
            {
                return 1;
            }

            return 0;
        }
    }
}
