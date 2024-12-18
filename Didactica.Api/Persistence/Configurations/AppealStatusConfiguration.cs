using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;

public class AppealStatusConfiguration : IEntityTypeConfiguration<AppealStatus>
{
    public void Configure(EntityTypeBuilder<AppealStatus> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
    }
}