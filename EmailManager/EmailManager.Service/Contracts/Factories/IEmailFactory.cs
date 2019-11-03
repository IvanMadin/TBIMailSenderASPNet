using System;
using EmailManager.Data.Entities;

namespace EmailManager.Service.Contracts.Factories
{
    public interface IEmailFactory
    {
        Email CreateEmail(string originalMailId, string sender, string dateReceived, string subject, string body);
    }
}