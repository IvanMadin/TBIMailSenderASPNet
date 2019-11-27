using EmailManager.Data.Entities;
using EmailManager.Data.Entities.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EmailManager.Data.Configurations
{
    internal class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
        {
            builder.HasKey(la => la.Id);

            builder
                .HasOne(la => la.User)
                .WithMany(u => u.LoanApplications)
                .HasForeignKey(la => la.UserId).IsRequired(false);


            builder
                .HasOne(la => la.ClientData)
                .WithMany(cd => cd.LoanApplications)
                .HasForeignKey(la => la.ClientDataId).IsRequired(false);

            builder
                .HasOne(la => la.Email)
                .WithOne(e => e.LoanApplication)
                .HasForeignKey<LoanApplication>(la => la.EmailId).IsRequired(false);

            builder
                .Property(la => la.Amount)
                .HasColumnType("numeric(15,2)")
                .IsRequired();

            builder
                .Property(la => la.ApplicationStatus)
                .HasConversion(result => result.ToString(), parse => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), parse))
                .IsRequired();
        }
    }
}