using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfTestClient.ServiceReference1;

namespace WcfTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();
            ExtentionData ext = new ExtentionData() { ImportantData = true };
            ExtentionData res = client.GetExtData(ext);

            ExtType et = client.GetAdditionalDAta(new ExtType() { Name = "test", ImportantData = true });
            Console.WriteLine(res.OptionalData);
            Console.WriteLine(et.Name);
        }
    }
}
