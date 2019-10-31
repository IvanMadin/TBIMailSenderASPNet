using EmailManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailManager.Data.Configurations
{
    internal class StatusApplicationConfiguration : IEntityTypeConfiguration<StatusApplication>
    {
        public void Configure(EntityTypeBuilder<StatusApplication> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder
                .HasMany(sa => sa.LoanApplications)
                .WithOne(la => la.StatusApplication)
                .HasForeignKey(la => la.StatusApplicationId);

            builder
                .Property(sa => sa.StatusType)
                .IsRequired();
        }
    }
}