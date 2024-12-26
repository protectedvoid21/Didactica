using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

public class LessonType : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
}