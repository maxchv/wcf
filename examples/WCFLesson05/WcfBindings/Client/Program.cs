using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            ServiceReference1.PrivateServiceClient clientPrivate = new ServiceReference1.PrivateServiceClient("NetTcpBinding_IPrivateService");

            ServiceReference1.PublicServiceClient clientPublic = new ServiceReference1.PublicServiceClient("BasicHttpBinding_IPublicService");

            Console.WriteLine(clientPrivate.GetPrivateData(""));
            Console.WriteLine(clientPublic.GetPublicData(""));
        }
    }
}
