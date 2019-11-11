using EmailManager.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Contracts.Factories
{
    public interface IClientDataFactory
    {
        ClientDataDTO Create(string firstName, string lastName, string egn, string phone, string operatorId);
    }
}
