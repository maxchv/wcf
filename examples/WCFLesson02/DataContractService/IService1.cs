using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DataContractService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceShop
    {

        [OperationContract]
        void AddCommoity(Commodity com);

        [OperationContract]
        Commodity GetCommodity(int id);
    }
        
    [DataContract]
    public class Commodity
    {

        private int id;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string category;
        [DataMember]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private decimal price;
        [DataMember]
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
