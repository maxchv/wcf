using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RentAutoWcf;

namespace RentHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost service = new ServiceHost(typeof(RentService)))
            {
                service.Open();
                Console.WriteLine("Service is ready");
                Console.ReadKey();
            }
        }
    }
}
