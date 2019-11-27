using System.ComponentModel.DataAnnotations;

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
        [RegularExpression("[0-9]+", ErrorMessage = "EGN can only contains Digits")]
        public string EGN { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "{0} cannot be less than {2} and more than {1} characters.", MinimumLength = 10)]
        [RegularExpression("[0-9]+", ErrorMessage = "Phone numbers can only contains Digits")]
        public string Phone { get; set; }
        [Required]
        [Range(0, 200000000,ErrorMessage ="{0} cannot be negative number.")]
        public decimal Amount { get; set; }
        public string EmailId { get; set; }
        public string Status { get; set; }
    }
}
