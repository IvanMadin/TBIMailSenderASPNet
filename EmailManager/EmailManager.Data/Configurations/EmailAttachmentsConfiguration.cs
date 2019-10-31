using EmailManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailManager.Data.Configurations
{
    internal class EmailAttachmentsConfiguration : IEntityTypeConfiguration<EmailAttachments>
    {
        public void Configure(EntityTypeBuilder<EmailAttachments> builder)
        {
            builder.HasKey(ea => ea.Id);

            builder.HasOne(ea => ea.Email)
                .WithMany(e => e.EmailAttachments)
                .HasForeignKey(ea => ea.EmailId);

            builder
                .Property(ea => ea.FileName)
                .IsRequired();

            builder
                .Property(ea => ea.FileSize)
                .IsRequired();
        }
    }
}