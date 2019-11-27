using EmailManager.Data.Entities.BaseProperties;
using EmailManager.Data.Entities.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class LoanApplication : ModifierEntityProperties
    {
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public string ChangedCloseToNewStatusForEmailId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string ClientDataId { get; set; }
        public ClientData ClientData { get; set; }

        public string EmailId { get; set; }
        public ClientEmail Email { get; set; }

    }
}
