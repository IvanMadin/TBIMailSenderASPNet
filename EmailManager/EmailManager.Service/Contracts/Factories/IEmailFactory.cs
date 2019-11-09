using System;
using EmailManager.Data.Entities;

namespace EmailManager.Service.Contracts.Factories
{
    public interface IEmailFactory
    {
        ClientEmail CreateEmail(string originalMailId, string senderName, string senderEmail, string dateReceived, string subject, string body);
    }
}