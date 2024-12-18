using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;


public class HospitationConfiguration : IEntityTypeConfiguration<Hospitation>
{
    public void Configure(EntityTypeBuilder<Hospitation> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.HospitationDate).IsRequired();
        builder.HasOne(h => h.HospitationMethod)
            .WithMany()
            .HasForeignKey("HospitationMethodId")
            .IsRequired();
    }
}