namespace Didactica.Persistence.Entities;

public class Appeal
{
    public int Id { get; private set; }
    public DateOnly SubmissionDate { get; set; }
    public DateOnly ConsiderationDate { get; set; }
    public required AppealStatus Status { get; set; }
    public String? Justification { get; set; }
}