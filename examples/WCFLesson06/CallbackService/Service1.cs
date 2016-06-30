using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace CallbackService
{    
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CallbackService : ICallbackService
    {
        public void Start()
        {
            for(int i=0; i<=100; i++)
            {
                Thread.Sleep(100);
                OperationContext.Current.GetCallbackChannel<INotify>().Notify(i);
            }
        }
    }
}
