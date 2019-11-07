using EmailManager.Data.Entities.BaseProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class ClientData : ModifierEntityProperties
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EncryptedEGN { get; set; }
        public string EncryptedPhone { get; set; }

        public ICollection<LoanApplication> LoanApplications { get; set; }
    }
}
