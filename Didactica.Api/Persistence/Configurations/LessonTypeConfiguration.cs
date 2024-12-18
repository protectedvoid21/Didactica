using Didactica.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Didactica.Api.Persistence.Configurations;

public class LessonTypeConfiguration : IEntityTypeConfiguration<LessonType>
{
    public void Configure(EntityTypeBuilder<LessonType> builder)
    {
        builder.HasKey(lt => lt.Id);
        builder.Property(lt => lt.Name).IsRequired();
    }
}