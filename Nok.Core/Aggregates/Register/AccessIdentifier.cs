using Nok.Core.Enums;
using Nok.Core.Extensions;

namespace Nok.Core.Aggregates.Register;

public class AccessIdentifier : GuidDataEntity
{
    public Guid AzureOid { get; }
    public AccessIdentifierType Type { get; }
    public virtual IList<Member> Members { get; init; } = [];

    private AccessIdentifier()
    {
        // Required by EF
    }

    public AccessIdentifier(Guid id, Guid azureOid, AccessIdentifierType type)
    {
        Id = id;
        AzureOid = azureOid;
        Type = type;

        CreatedBy = Id;
        CreatedDate = SystemTime.UtcNow();
        UpdatedBy = Id;
        UpdatedDate = SystemTime.UtcNow();
    }
}
