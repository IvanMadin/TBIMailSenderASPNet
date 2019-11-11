using EmailManager.Data;
using EmailManager.Data.Entities;
using EmailManager.Service.Contracts;
using EmailManager.Service.DTOs;
using EmailManager.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Service
{
    public class ApplicationStatusService : IApplicationStatusService
    {
        private readonly EmailManagerDbContext context;
        public ApplicationStatusService(EmailManagerDbContext context)
        {
            this.context = context;
        }
        // maybe is not necessary
        public async Task<StatusApplicationDTO> CreateApplicationStatusAsync(string statusApplication)
        {
            var status = new StatusApplication() { StatusType = statusApplication };

            await this.context.SaveChangesAsync();
            return status.ToDTO();
        }

        public async Task<StatusApplicationDTO> GetApplicationStatusByIdAsync(string statusApplicationId)
        {
            var statusApplication = await this.context.StatusApplications.FindAsync(statusApplicationId);

            if (statusApplication == null)
            {
                Log.Error("Application status is null");
                throw new Exception("Application status is null");
            }

            Log.Information("Application with status ID: {0} was successfully taken!", statusApplicationId);
            return statusApplication.ToDTO();
        }


        public async Task<StatusApplicationDTO> UpdateApplicationStatusAsync(LoanApplicationDTO loanApplication, string newStatusApplication)
        {
            var applicationStatus = await this.context.StatusApplications.FirstOrDefaultAsync(x => x.StatusType == newStatusApplication);

            if(applicationStatus is null)
            {
                throw new Exception("Application status is not found");
            }

            var application = await this.context.LoanApplications.FindAsync(loanApplication.Id);

            if(application is null)
            {
                throw new Exception("Application is not found");
            }

            application.StatusApplicationId = applicationStatus.Id;
            await this.context.SaveChangesAsync();
            return applicationStatus.ToDTO();
        }

        public async Task<ICollection<StatusApplicationDTO>> AllApplicationStatusAsync()
        {
            var statusApplication = await this.context.StatusApplications.ToListAsync();

            return statusApplication.ToDTO();
        }

    }
}
