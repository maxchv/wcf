using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfBindings
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PrivatePublicService : IPrivateService, IPublicService
    {
        public string GetPrivateData(string msg)
        {
            return "It is secret message";
        }

        public string GetPublicData(string msg)
        {
            return "It is public message";
        }
    }
}
