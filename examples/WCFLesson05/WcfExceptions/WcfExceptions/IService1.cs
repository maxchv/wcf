using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfExceptions
{
    [ServiceContract]
    public interface IServiceCalculator
    {
        [FaultContract(typeof(DevideByZeroExeption))]
        [OperationContract]
        int Div(int a, int b);
    }

    [DataContract]
    public class DevideByZeroExeption
    {
        
        private string error;

        [DataMember]
        public string Error
        {
            get { return error; }
            set { error = value; }
        }
        
        private string detalis;

        [DataMember]
        public string Details
        {
            get { return detalis; }
            set { detalis = value; }
        }


    }
        
    
}
