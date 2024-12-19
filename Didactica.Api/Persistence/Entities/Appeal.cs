using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Didactica.Persistence.Entities;

public class Appeal: BaseTrackingEntity
{
    public DateOnly SubmissionDate { get; set; }
    public DateOnly ConsiderationDate { get; set; }
    public required AppealStatus Status { get; set; }
    [MaxLength(1000)]
    public String? Justification { get; set; }
}