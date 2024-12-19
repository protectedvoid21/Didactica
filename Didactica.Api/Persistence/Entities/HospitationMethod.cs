using System.ComponentModel.DataAnnotations;

namespace Didactica.Persistence.Entities;

public class HospitationMethod: BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }
}