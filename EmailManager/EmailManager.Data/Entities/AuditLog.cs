using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class AuditLog
    {
        public string Id { get; set; }

        public ICollection<LoanApplication> LoanApplications { get; set; }
    }
}
