using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class Email
    {
        public string Id { get; set; }

        public string Sender { get; set; }
        public DateTime DateReceived { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public ICollection<EmailAttachments> EmailAttachments { get; set; }

        public string StatusEmailId { get; set; }
        public StatusEmail StatusEmail { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public LoanApplication LoanApplication { get; set; }
    }
}
