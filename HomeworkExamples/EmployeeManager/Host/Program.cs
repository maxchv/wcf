using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost service = new ServiceHost(typeof(ManagerService)))
            {
                service.Open();
                Console.WriteLine("Service is ready");
                Console.ReadKey();
            }
        }
    }
}
