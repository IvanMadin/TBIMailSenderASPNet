using EmailManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailManager.Data.Configurations
{
    internal class StatusEmailConfiguration : IEntityTypeConfiguration<StatusEmail>
    {
        public void Configure(EntityTypeBuilder<StatusEmail> builder)
        {
            builder.HasKey(se => se.Id);

            builder.HasMany(se => se.Emails)
                .WithOne(e => e.Status)
                .HasForeignKey(e => e.StatusEmailId);

            builder
                .Property(se => se.StatusType)
                .IsRequired();
        }
    }
}