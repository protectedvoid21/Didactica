using System.ComponentModel.DataAnnotations;

namespace Didactica.Persistence.Entities;

public class AppealStatus: BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
}