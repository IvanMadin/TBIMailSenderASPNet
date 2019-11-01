using EmailManager.Data.Configurations;
using EmailManager.Data.DataSeeder;
using EmailManager.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data
{
    public class EmailManagerDbContext : IdentityDbContext<User>
    {
        public EmailManagerDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Email> Emails { get; set; }
        public DbSet<ClientData> ClientDatas { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<EmailAttachments> EmailAttachments { get; set; }
        public DbSet<StatusApplication> StatusApplications { get; set; }
        public DbSet<StatusEmail> StatusEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Email>(new EmailConfiguration());
            builder.ApplyConfiguration<ClientData>(new ClientDataConfiguration());
            builder.ApplyConfiguration<LoanApplication>(new LoanApplicationConfiguration());
            builder.ApplyConfiguration<EmailAttachments>(new EmailAttachmentsConfiguration());
            builder.ApplyConfiguration<StatusApplication>(new StatusApplicationConfiguration());
            builder.ApplyConfiguration<StatusEmail>(new StatusEmailConfiguration());

            builder.UserRoleSeeder();
            base.OnModelCreating(builder);
        }
    }
}
