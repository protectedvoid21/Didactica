namespace Didactica.Domain.Models;

public class Appeal : BaseTrackingEntity
{
    public DateOnly SubmissionDate { get; set; }
    public DateOnly ConsiderationDate { get; set; }
    public required AppealStatus Status { get; set; }
    public string? Justification { get; set; }
}