using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceRef.TestServiceClient client = new ServiceRef.TestServiceClient();
            string answer = client.GetAnswer("User");
            Console.WriteLine(answer);
        }
    }
}
