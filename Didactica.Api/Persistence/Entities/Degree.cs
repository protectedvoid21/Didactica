using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class Degree: BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(100)]
    public required string Short { get; set; }
}