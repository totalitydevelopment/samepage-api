using Nok.Core.Aggregates.Register;

namespace Nok.Core.Models;

public class GetMemberListItem : BaseId
{
    required public Name Name { get; init; }
    required public DateOfBirth? DateOfBirth { get; init; }
    required public string? KnownTown { get; init; }
    required public bool HasImage { get; init; }
}
