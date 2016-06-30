using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServiceHostShop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(DataContractService.Service1)))
            {
                host.Open();
                Console.WriteLine("Service started");
                Console.ReadLine();
            }
        }
    }
}
