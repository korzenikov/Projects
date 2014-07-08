using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class JobSchedulerTest
    {
        [TestMethod]
        public void CompareByDifferenceTest()
        {
            var jobs = new[] { new Job(3, 5), new Job(1, 2), new Job(4, 5) };
            var weightedTime = JobScheduler.GetWeightedSumOfCompletionTimes(jobs, JobScheduler.CompareByDifference);
            weightedTime.Should().Be(63);
        }
       
        [TestMethod]
        public void CompareByRatioTest()
        {
            var jobs = new[] { new Job(3, 5), new Job(1, 2), new Job(4, 5) };
            var weightedTime = JobScheduler.GetWeightedSumOfCompletionTimes(jobs, JobScheduler.CompareByRatio);
            weightedTime.Should().Be(62);
        }
    }
}
