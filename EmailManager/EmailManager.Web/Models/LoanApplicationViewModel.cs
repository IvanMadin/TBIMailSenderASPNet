using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Models
{
    public class LoanApplicationViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} cannot be less than {2} and more than {1} characters.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} cannot be less than {2} and more than {1} characters.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "{0} must be {1} characters.", MinimumLength = 10)]
        public string EGN { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "{0} cannot be less than {2} and more than {1} characters.", MinimumLength = 10)]
        public string Phone { get; set; }
        public decimal Amount { get; set; }
        public string EmailId { get; set; }
        public string Status { get; set; }
    }
}
