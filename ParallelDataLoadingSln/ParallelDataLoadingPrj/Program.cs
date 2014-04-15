namespace ParallelDataLoadingPrj
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var executor1 = new AsyncLazyBasedExecutor();
            executor1.IntializeAsync().Wait();

            var executor2 = new TaskBasedExecutor();
            executor2.IntializeAsync().Wait();
        }
    }
}
