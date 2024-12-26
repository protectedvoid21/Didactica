namespace Didactica.Domain.Models;

public abstract class BaseTrackingEntity : BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}