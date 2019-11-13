using EmailManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailManager.Data.Configurations
{
    internal class EmailConfiguration : IEntityTypeConfiguration<ClientEmail>
    {
        public void Configure(EntityTypeBuilder<ClientEmail> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.User)
                .WithMany(u => u.Emails)
                .HasForeignKey(u => u.UserId).IsRequired(false);

            builder.HasOne(e => e.LoanApplication)
                .WithOne(la => la.Email)
                .HasForeignKey<LoanApplication>(la => la.EmailId);

            builder.HasOne(e => e.Status)
                .WithMany(se => se.Emails)
                .HasForeignKey(e => e.StatusEmailId);

            builder.HasMany(e => e.EmailAttachments)
                .WithOne(ea => ea.Email)
                .HasForeignKey(ea => ea.EmailId);

            builder
                .Property(e => e.OriginalMailId)
                .IsRequired();
            builder
                .Property(e => e.SenderName)
                .IsRequired();

            builder
                .Property(e => e.SenderEmail)
                .IsRequired();

            builder
                .Property(e => e.DateReceived)
                .IsRequired();

            builder
                .Property(e => e.Subject)
                .IsRequired();

            builder
                .Property(e => e.Body);
        }
    }
}