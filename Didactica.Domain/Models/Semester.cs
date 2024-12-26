using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

public class Semester : BaseEntity
{
    [MaxLength(100)] 
    public required string Name { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}