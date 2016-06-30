using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfMsgLog
{   
    [ServiceContract]
    public interface IService1
    {
        [OperationContract(Name = "GetSimpleData")]
        string GetData(int value);

        [OperationContract(Name = "GetExtData")]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        ExtType GetAdditionalDAta(ExtType composite);
    }



    [KnownType(typeof(ExtType))]
    [KnownType(typeof(ExtType2))]
    [DataContract(Name = "ExtentionData", Namespace = "http://itstep.dp.ua/TestWCF")]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember(Name ="ImportantData", Order = 2, IsRequired = true)]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember(Name = "OptionalData", Order = 1, IsRequired = false)]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    public class ExtType: CompositeType
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class ExtType2 : CompositeType
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
