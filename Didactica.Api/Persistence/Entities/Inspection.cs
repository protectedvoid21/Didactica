using System.ComponentModel.DataAnnotations;

namespace Didactica.Api.Persistence.Entities;

public class Inspection : BaseTrackingEntity
{
    public DateTime Date { get; set; }
    public required InspectionMethod InspectionMethod { get; set; }
    public required Employee Employee { get; set; }
    public required Lesson Lesson { get; set; }
    public required bool IsRemote { get; set; }
    [MaxLength(100)]
    public string? LessonEnvironment { get; set; }
}