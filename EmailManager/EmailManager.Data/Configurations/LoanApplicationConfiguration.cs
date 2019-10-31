using EmailManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasForeignKey(la => la.UserId);

            builder
                .HasOne(la => la.ClientData)
                .WithMany(cd => cd.LoanApplications)
                .HasForeignKey(la => la.ClientDataId);

            builder
                .HasOne(la => la.Email)
                .WithOne(e => e.LoanApplication)
                .HasForeignKey<LoanApplication>(la => la.EmailId);

            builder
                .HasOne(la => la.StatusApplication)
                .WithMany(sa => sa.LoanApplications)
                .HasForeignKey(la => la.StatusApplicationId);
        }
    }
}