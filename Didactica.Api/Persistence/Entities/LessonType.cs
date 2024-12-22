using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class LessonType: BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
}