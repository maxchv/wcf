using System;
using System.ServiceModel;

namespace TestingSystemHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(TestingSystemService.TestingSystemService)))
                {
                    host.Open();

                    Console.WriteLine("Server is ready...");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}