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

                Console.WriteLine("Press <return> to exit.");
                Console.ReadLine();
            }
            else
                ServiceBase.Run(host);
        }
    }
}
