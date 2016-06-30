using Client.ServiceReference1;
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
            ServiceShopClient client = new ServiceShopClient();
            Commodity snikers = new Commodity()
            {
                Id = 1, Category = "Sweet", Name="Snikers", Price=8.5m
            };
            client.AddCommoity(snikers);

            Commodity c = client.GetCommodity(1);
            Console.WriteLine(c.Name);
        }
    }
}
