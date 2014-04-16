namespace ParallelDataLoadingPrj
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("AsyncLazy based loader");
            var loader1 = new AsyncLazyBasedLoader();
            loader1.LoadAsync().Wait();

            Console.WriteLine("Task based loader");
            var loader2 = new TaskBasedLoader();
            loader2.LoadAsync().Wait();
        }
    }
}