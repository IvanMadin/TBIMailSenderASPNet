using EmailManager.GmailConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailManager.Service.Utilities
{
    public class AutomatedService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;
        public AutomatedService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(RunEmailChecker, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(2));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async void RunEmailChecker(object state)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var gmailService = scope.ServiceProvider.GetRequiredService<GmailConfigure>();
                await gmailService.GmailAPI();
            }
        }
    }
}
