using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models;

/// <summary>
/// Represents an appeal entity typically associated with a review or evaluation process.
/// </summary>
/// <remarks>
/// This class inherits from <see cref="BaseTrackingEntity"/>, providing
/// tracking properties such as creation and update timestamps.
/// </remarks>
public class Appeal : BaseTrackingEntity
{
    public DateOnly SubmissionDate { get; set; }
    public DateOnly ConsiderationDate { get; set; }
    public required AppealStatus Status { get; set; }
    [MaxLength (1000)]
    public string? Justification { get; set; }
}