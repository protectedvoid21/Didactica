using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;


public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.LessonCode).IsRequired();
        builder.Property(l => l.LessonName).IsRequired();
        builder.Property(l => l.LessonDate).IsRequired();
        builder.Property(l => l.Room).HasMaxLength(100);
        builder.HasOne(l => l.LessonType)
            .WithMany()
            .HasForeignKey("LessonTypeId")
            .IsRequired();
    }
}