﻿using EmailManager.Data.Entities;
using EmailManager.Service.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Factories
{
    public class EmailFactory : IEmailFactory
    {
        public Email CreateEmail(string originalMailId, string sender, string dateReceived, string subject, string body)
        {
            var newEmail = new Email
            {
                OriginalMailId = originalMailId,
                Sender = sender,
                DateReceived = dateReceived,
                Subject = subject,
                Body = body,
                StatusEmailId = "a0e53404-d40e-4a1e-8fe5-9a5fc0139ed9"
            };

            return newEmail;
        }
    }
}
