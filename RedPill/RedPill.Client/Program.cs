using RedPill.Client.ServiceReference1;

namespace RedPill.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedPillClient("BasicHttpBinding_IRedPill");

            var result = client.FibonacciNumber(93);
            var isosceles = client.WhatShapeIsThis(2, 2, 3);
            var rev = client.ReverseWords("And I think to myself, what a wonderful world.");
            var rev2 = client.ReverseWords(null);
            client.Close();
        }
    }
}
