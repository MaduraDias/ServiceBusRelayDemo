﻿using Accounting.Service.Contracts;
using System.ServiceModel;

namespace Accounting.Console
{
    public interface IAccountServiceChannel : IAccountService, IClientChannel
    {
    }
}