using System.ComponentModel.DataAnnotations;

namespace Nok.Infrastructure.Data.Models;

public class AccessIdentifier
{
    [Key]
    public Guid Id { get; init; }
    public Guid AzureOid { get; init; }
    public AccessIdentifierType Type { get; init; }
    public virtual IList<Member> Members { get; init; } = [];
}
