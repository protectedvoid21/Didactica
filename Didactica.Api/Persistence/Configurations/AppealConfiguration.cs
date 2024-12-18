using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;

public class AppealConfiguration: IEntityTypeConfiguration<Appeal>
{
    public void Configure(EntityTypeBuilder<Appeal> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasOne<AppealStatus>(u => u.Status)
            .WithMany()
            .HasForeignKey("Id")
            .IsRequired();
        builder.Property(u => u.Status)
            .HasConversion<string>()
            .IsRequired();
        builder.Property(u => u.Justification)
            .HasMaxLength(1000)
            .IsRequired(false);
    }
}