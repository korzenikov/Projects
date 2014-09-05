using RedPill.Client.ServiceReference1;

namespace RedPill.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedPillClient();

            var result = client.FibonacciNumber(1);
            client.Close();
        }
    }
}
