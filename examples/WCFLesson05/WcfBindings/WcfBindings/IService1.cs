using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfBindings
{   
    [ServiceContract]
    public interface IPublicService
    {
        [OperationContract]
        string GetPublicData(string msg);
    }

    [ServiceContract]
    public interface IPrivateService
    {
        [OperationContract]
        string GetPrivateData(string msg);
    }


}
