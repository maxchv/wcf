using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DataContractService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IServiceShop
    {
        List<Commodity> allCommodity = new List<Commodity>();
        public void AddCommoity(Commodity com)
        {
            allCommodity.Add(com);
            Console.WriteLine("Added new commodity");
        }

        public Commodity GetCommodity(int id)
        {
            return allCommodity.Find(c => c.Id == id);
        }
    }
}
