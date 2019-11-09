using EmailManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailManager.Data.Configurations
{
    internal class ClientDataConfiguration : IEntityTypeConfiguration<ClientData>
    {
        public void Configure(EntityTypeBuilder<ClientData> builder)
        {
            builder.HasKey(cd => cd.Id);

            builder
                .HasMany(cd => cd.LoanApplications)
                .WithOne(la => la.ClientData)
                .HasForeignKey(la => la.ClientDataId);

            builder
                .Property(cd => cd.FirstName)
                .IsRequired();

            builder
                .Property(cd => cd.EGN)
                .IsRequired();

            builder
                .Property(cd => cd.Phone)
                .IsRequired();
        }
    }
}