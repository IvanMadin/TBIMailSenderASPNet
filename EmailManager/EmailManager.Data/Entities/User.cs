using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class User : IdentityUser
    {
        public ICollection<Email> Emails { get; set; }
        public ICollection<LoanApplication> LoanApplications { get; set; }
    }
}
