using EmailManager.Data.Entities.BaseProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class ClientEmail : IModified
    {
        public string Id { get; set; }
        public string OriginalMailId { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public DateTime DateReceived { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ModifiedByUserId { get; set; }
        public DateTime? ModifiedOnDate { get; set; }

        public ICollection<EmailAttachments> EmailAttachments { get; set; }

        public string StatusEmailId { get; set; }
        public StatusEmail Status { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public LoanApplication LoanApplication { get; set; }
    }
}
