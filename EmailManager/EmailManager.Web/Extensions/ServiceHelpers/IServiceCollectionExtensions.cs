using EmailManager.GmailConfig;
using EmailManager.Service;
using EmailManager.Service.Contracts;
using EmailManager.Service.Contracts.Factories;
using EmailManager.Service.Factories;
using EmailManager.Service.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Web.Extensions.ServiceHelpers
{
    public static class IServiceCollectionExtensions
    {
        public static void Registrations(this IServiceCollection services)
        {
            services.AddScoped<GmailConfigure>();
            services.AddScoped<IEmailFactory, EmailFactory>();
            services.AddScoped<IEncryptingHelper, EncryptingHelper>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddTransient<IEmailStatusService, EmailStatusService>();
            services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            services.AddTransient<IClientDataFactory, ClientDataFactory>();
            services.AddScoped<IValidation, Validation>();
            services.AddScoped<IRolesService, UsersService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAttachmentsService, AttachmentsService>();
            services.AddScoped<IEGNChecker, EGNChecker>();
        }
    }
}
