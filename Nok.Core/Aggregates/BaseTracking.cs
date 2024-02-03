namespace Nok.Core.Aggregates;

public abstract class BaseTracking
{
    public DateTime CreatedDate { get; protected set; }
    public Guid CreatedBy { get; protected set; }
    public DateTime UpdatedDate { get; protected set; }
    public Guid UpdatedBy { get; protected set; }
}
