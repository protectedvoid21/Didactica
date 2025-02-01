namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents a team responsible for inspections within the system.
/// </summary>
/// <remarks>
/// The <c>InspectionTeam</c> class is an entity that inherits from <see cref="BaseTrackingEntity"/>.
/// It represents a group of teachers who collaboratively carry out inspections.
/// This entity includes a collection of <see cref="Teacher"/> objects assigned to the team, as well as
/// an optional collection of <see cref="Inspection"/> objects associated with the team.
/// </remarks>
/// <example>
/// This class is used to manage and organize teams performing inspection tasks and is persisted in the database.
/// </example>
public class InspectionTeam : BaseTrackingEntity
{
    public ICollection<Teacher> Teachers { get; set; } = [];
    public ICollection<Inspection>? Inspections { get; set; }
}