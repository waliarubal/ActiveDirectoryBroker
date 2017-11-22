using ActiveDirectoryBroker.Services;
using System;
using System.ServiceProcess;

namespace ActiveDirectoryBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new BrokerHost();
            if (Environment.UserInteractive)
            {
                host.StartService();

                Console.WriteLine("Web service started successfully, press <return> to stop.");
                Console.ReadLine();
            }
            else
                ServiceBase.Run(host);
        }
    }
}
