using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class InspectionMethod : BaseEntity
{
    [MaxLength(255)]
    public required string Name { get; set; }
}