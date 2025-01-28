namespace Didactica.Domain.Models;

public class InspectionForm : BaseTrackingEntity
{
    public required int InspectionId { get; set; }
    public Inspection Inspection { get; set; } = null!;
    
    public int? AppealId { get; set; }
    public Appeal? Appeal { get; set; }
    
    public required bool WereClassesOnTime { get; set; }
    public required bool WasAttendanceChecked { get; set; }
    public required bool WasRoomSuitable { get; set; }

    public required int PresentedTopicAndScope { get; set; }
    public required int ExplainedClearly { get; set; }
    public required int WasEngaged { get; set; }
    public required int EncouragedIndependentThinking { get; set; }
    public required int MaintainedDocumentation { get; set; }
    public required int DeliveredUpdatedKnowledge { get; set; }
    public required int PresentedPreparedMaterial { get; set; }

    public required string FinalGradeJustification { get; set; }
    public required string ConclusionsAndRecommendations { get; set; }
    public required string FinalGrade { get; set; }
}