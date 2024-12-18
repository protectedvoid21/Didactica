using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;

public class HospitationMethodConfiguration : IEntityTypeConfiguration<HospitationMethod>
{
    public void Configure(EntityTypeBuilder<HospitationMethod> builder)
    {
        builder.HasKey(hm => hm.Id);
        builder.Property(hm => hm.Name).IsRequired();
    }
}