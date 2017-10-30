using System;
using System.ServiceModel;

namespace CalculatorServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService.CalculatorService)))
            {
                host.Open();

                Console.WriteLine("Server is ready...");
                Console.ReadKey();
            }
        }
    }
}