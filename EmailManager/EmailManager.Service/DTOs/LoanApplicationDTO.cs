using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.DTOs
{
    public class LoanApplicationDTO
    {
        public string Id { get; set; }
        public string ClientFristName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientEGN { get; set; }
        public string ClientPhone { get; set; }
        public decimal LoanAmount { get; set; }
        public string EmployeeName { get; set; }
        public string EmailId { get; set; }
    }
}
