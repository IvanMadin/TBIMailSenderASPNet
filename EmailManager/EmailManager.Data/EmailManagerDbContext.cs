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

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<StatusApplication> StatusApplications { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            base.OnModelCreating(builder);
        }
    }
}
