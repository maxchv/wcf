using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloWCF
{    
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string GetAnswer(string msg);

        void OtherMethod();
    }
}
