using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class LoanApplication
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string EmailId { get; set; }
        public Email Email { get; set; }

        public string StatusApplicationId { get; set; }
        public StatusApplication StatusApplication { get; set; }
    }
}
