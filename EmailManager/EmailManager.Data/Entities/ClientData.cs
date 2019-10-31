using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class ClientData
    {
        public string Id { get; set; }
        public string Names { get; set; }
        public string EncryptedEGN { get; set; }
        public string EncryptedPhone { get; set; }
        public string EncryptedEmail { get; set; }

        public ICollection<LoanApplication> LoanApplications { get; set; }
    }
}
