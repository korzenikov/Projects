﻿namespace CourseraTasks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITask task = new FloydWarshallTask();
            task.Run();
        }
    }
}
