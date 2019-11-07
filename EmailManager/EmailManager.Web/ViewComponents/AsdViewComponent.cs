using EmailManager.Service;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.ViewComponents
{
    public class AsdViewComponent : ViewComponent
    {
        private readonly EmailService emailService;

        public AsdViewComponent(EmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(EmailViewModel emailView)
        {
            var email = await this.emailService.GetEmailByIdAsync(emailView.Id);
            var newLoanApplication = new LoanApplicationViewModel
            {
                EmailId = email.Id
            };

            return View(newLoanApplication);
        }
    }
}
