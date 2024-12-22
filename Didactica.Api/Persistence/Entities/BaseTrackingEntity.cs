namespace Didactica.Api.Persistence.Entities;

public abstract class BaseTrackingEntity : BaseEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}