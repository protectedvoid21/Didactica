using System.ComponentModel.DataAnnotations;

namespace Didactica.Domain.Models.Persistent;

public class InspectionForm : BaseTrackingEntity
{
    public required int InspectionId { get; set; }
    public Inspection Inspection { get; set; } = null!;
    
    public int? AppealId { get; set; }
    public Appeal? Appeal { get; set; }
    
    public required bool WereClassesOnTime { get; set; }
    public required bool WasAttendanceChecked { get; set; }

    /// <summary>
    /// Indicates whether the room used during the inspected session was suitable for the lesson's requirements.
    /// </summary>
    /// <remarks>
    /// This property evaluates the adequacy of the physical or virtual learning environment, considering factors
    /// such as cleanliness, safety, accessibility, and appropriateness for the subject matter.
    /// </remarks>
    public required bool WasRoomSuitable { get; set; }

    public required int PresentedTopicAndScope { get; set; }
    public required int ExplainedClearly { get; set; }
    public required int WasEngaged { get; set; }
    public required int EncouragedIndependentThinking { get; set; }
    public required int MaintainedDocumentation { get; set; }
    public required int DeliveredUpdatedKnowledge { get; set; }
    public required int PresentedPreparedMaterial { get; set; }
    [MaxLength (1000)]
    public required string FinalGradeJustification { get; set; }
    [MaxLength (1000)]
    public required string ConclusionsAndRecommendations { get; set; }
    [MaxLength (100)]
    public required string FinalGrade { get; set; }
}