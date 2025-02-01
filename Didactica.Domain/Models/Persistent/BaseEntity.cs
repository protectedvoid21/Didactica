namespace Didactica.Domain.Models.Persistent;

/// <summary>
/// Represents the base class for all entities in the system.
/// </summary>
/// <remarks>
/// The BaseEntity class defines a foundational structure for all entities within the system.
/// It provides a unique identifier property, `Id`, shared by all derived classes.
/// This class is intended to be abstract and cannot be instantiated directly.
/// </remarks>
public abstract class BaseEntity
{
    public int Id { get; set; }
}