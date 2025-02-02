using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents an inspection conducted for a particular lesson and teacher.
/// </summary>
/// <remarks>
/// This class is used to encapsulate the details of an inspection, such as the associated teacher and lesson,
/// whether the inspection was conducted remotely, the inspection environment, and the inspection team details.
/// It inherits from <see cref="BaseTrackingEntity"/>, providing tracking capabilities for created and updated timestamps.
/// </remarks>
public class Inspection : BaseTrackingEntity
{
    public required Teacher Teacher { get; set; }
    public required Lesson Lesson { get; set; }
    public required bool IsRemote { get; set; }
    [MaxLength(100)]
    public string? LessonEnvironment { get; set; }
    public InspectionTeam? InspectionTeam { get; set; }
    public int InspectionTeamId { get; set; }
    
    public InspectionForm? InspectionForm { get; set; }
    public int? InspectionFormId { get; set; }
}