using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HelloWCF
{
    public class TestService : ITestService
    {
        public string GetAnswer(string msg)
        {
            return "Hello " + msg;
        }

        public void OtherMethod()
        {
            
        }
    }
}
