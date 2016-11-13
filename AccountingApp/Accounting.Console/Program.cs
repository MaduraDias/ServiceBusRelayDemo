using Accounting.Service.Contracts;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Tcp;
            var cf = new ChannelFactory<IAccountServiceChannel>("AccountService");

            var accountBalance = 0d;
            var serviceId = "0";

            var taskList = new List<Task<AccountBalanceReponse>>();
            IAccountServiceChannel ch;
            try
            {
                ch = cf.CreateChannel();
                //using (var ch = cf.CreateChannel())
                //{
                for (int i = 0; i < 100; i++)
                {
                    taskList.Add(Task.Run(() => ch.GetAccountBalance(100)));
                }
                // }



                foreach (Task<AccountBalanceReponse> task in taskList)
                {

                    System.Console.WriteLine("Balance {0}  From  Service Id : {1}", task.Result.AccountBalance, task.Result.ServiceId);
                }
            }
            finally
            {
                
            }
            System.Console.ReadLine();

        }
    }
}
