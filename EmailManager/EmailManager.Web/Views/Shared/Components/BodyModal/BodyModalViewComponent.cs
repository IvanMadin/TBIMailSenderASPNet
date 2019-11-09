using EmailManager.Service;
using EmailManager.Web.Extensions.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Views.Email.BodyModal
{
    public class BodyModalViewComponent : ViewComponent
    {
        private readonly EmailService emailService;
        public BodyModalViewComponent(EmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var email = await emailService.GetEmailByIdAsync(id);

           return View("BodyModal", email.ToVM());
        }
    }
}
