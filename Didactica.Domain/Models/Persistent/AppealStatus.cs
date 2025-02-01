using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents the status of an appeal process in the system.
/// </summary>
/// <remarks>
/// The <see cref="AppealStatus"/> class defines the state or condition
/// of an appeal at a given time. It is used to categorize and track
/// the progress or resolution of appeals within the system.
/// </remarks>
public class AppealStatus : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
}