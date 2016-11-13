using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Microsoft.ServiceBus.NetTcpRelaySecurity       
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Tcp;

            ServiceHost sh = new ServiceHost(typeof(AccountService));

            sh.Open();
            Console.WriteLine("Press ENTER to close");
            Console.ReadLine();
            sh.Close();
        }
    }
}
