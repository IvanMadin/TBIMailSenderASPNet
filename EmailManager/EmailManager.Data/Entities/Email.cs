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
        public double TotalSumMb { get; set; }

        //public string LoanApplicationId { get; set; }
        public LoanApplication LoanApplication { get; set; }
    }
}
