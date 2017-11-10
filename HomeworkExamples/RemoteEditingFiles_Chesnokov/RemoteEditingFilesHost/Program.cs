using System;
using System.ServiceModel;

namespace RemoteEditingFilesHost
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(RemoteEditingFilesService.RemoteEditingFilesService)))
                {
                    host.Open();
                
                    Console.WriteLine("Server is ready...");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}