using RedPill.Client.ServiceReference1;

namespace RedPill.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedPillClient("BasicHttpBinding_IRedPill");

            var result = client.FibonacciNumber(-93);
            client.Close();
        }
    }
}
