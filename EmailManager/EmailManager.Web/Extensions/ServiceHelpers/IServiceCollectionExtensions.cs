using EmailManager.GmailConfig;
using EmailManager.Service;
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
            services.AddScoped<EmailService>();
            services.AddScoped<LoanApplicationService>();
            services.AddScoped<ClientService>();
            services.AddScoped<EncryptingHelper>();
            services.AddScoped<ClientDataDTOFactory>();
        }
    }
}
