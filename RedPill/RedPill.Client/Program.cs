using RedPill.Client.ServiceReference1;

namespace RedPill.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RedPillClient client = new RedPillClient("BasicHttpBinding_IRedPill");
            var equilateral = client.WhatShapeIsThis(5, 5, 5);
            var scalene = client.WhatShapeIsThis(3, 4, 5);
            var isosceles = client.WhatShapeIsThis(2, 2, 3);
            var error = client.WhatShapeIsThis(2, 2, 4);
            client.Close();
        }
    }
}
