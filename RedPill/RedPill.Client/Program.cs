using System;
using RedPill.Client.ServiceReference1;

namespace RedPill.Client
{
    public static class Program
    {
        static void Main(string[] args)
        {
            using (var client = new RedPillClient("MyService"))
            {
                var result = client.WhatShapeIsThis(int.MaxValue, int.MaxValue, int.MaxValue);
                Console.WriteLine(result);
            }

            using (var client = new RedPillClient("TestService"))
            {
                var result = client.WhatShapeIsThis(int.MaxValue, int.MaxValue, int.MaxValue);
                Console.WriteLine(result);
            }
        }
    }
}
