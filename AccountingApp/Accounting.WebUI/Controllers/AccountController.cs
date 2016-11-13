using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Accounting.WebUI.Models;
using Microsoft.ServiceBus;
using System.ServiceModel;
using Accounting.Service.Contracts;
using Accounting.WebUI.Channels;

namespace Accounting.WebUI.Controllers
{

    public class AccountController : Controller
    {
        [HttpPost]
        public ViewResult GetBalance(long accountNumber)
        {
            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Tcp;
            var cf = new ChannelFactory<IAccountServiceChannel>("AccountService");
            
            var accountBalance = 0d;
            var serviceId = "0";
            using (var ch = cf.CreateChannel())
            {
                var response = ch.GetAccountBalance(accountNumber);
                accountBalance = response.AccountBalance;
                serviceId = response.ServiceId;
            }
            
            return View("AccountBalance",
                new AccountViewModel()
                {
                    AccountBalance = accountBalance,
                    AccountNumber = accountNumber,
                    ServiceId = serviceId
                });
        }

        [HttpGet]
        public ViewResult Detail()
        {
            return View("AccountBalance", new AccountViewModel());
        }
    }
}