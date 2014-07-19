namespace CourseraTasks.CSharp
{
    public class Job
    {
        public Job(int weight, int length)
        {
            Weight = weight;
            Length = length;
        }

        public int Weight { get; private set; }

        public int Length { get; private set; }
    }
}
