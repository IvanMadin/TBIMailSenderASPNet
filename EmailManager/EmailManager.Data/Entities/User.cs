using EmailManager.Data.Entities.BaseProperties;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class User : IdentityUser, IDeleted
    {
        public ICollection<ClientEmail> Emails { get; set; }
        public ICollection<LoanApplication> LoanApplications { get; set; }
        public bool ChangedPassword { get; set; }
        public string DeletedByUserId { get; set; }
        public DateTime? DeletedOnDate { get; set; }
    }
}
