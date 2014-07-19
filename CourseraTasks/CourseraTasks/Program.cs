namespace CourseraTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITask task = new ClusteringBigTask();
            task.Run();
        }
    }
}
