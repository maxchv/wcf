using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CallbackService
{   
    [ServiceContract(CallbackContract = typeof(INotify))]
    public interface ICallbackService
    {        
        [OperationContract(IsOneWay = true)]
        void Start();
    }

    public interface INotify
    {
        [OperationContract(IsOneWay = true)]
        void Notify(int percent);
    }
}
