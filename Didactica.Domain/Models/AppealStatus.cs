using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

public class AppealStatus : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
}