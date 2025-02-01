namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents the base entity with tracking capabilities for created and updated timestamps.
/// </summary>
/// <remarks>
/// This abstract class extends <see cref="BaseEntity"/> and adds properties to track
/// the creation and updating of entities. It is intended to be used as a base class
/// for entities requiring audit information.
/// </remarks>
public abstract class BaseTrackingEntity : BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}