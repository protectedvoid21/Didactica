using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasOne(s => s.Degree)
            .WithMany()
            .HasForeignKey("DegreeId")
            .IsRequired();
        builder.Property(s => s.SpecializationName).HasMaxLength(255);
    }
}