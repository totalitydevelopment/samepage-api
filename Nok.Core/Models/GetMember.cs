using Nok.Core.Aggregates.Register;

namespace Nok.Core.Models;

public class GetMember : BaseId
{
    public GetMember()
    {
        NextOfKins = [];
    }

    required public string? ImageUrl { get; init; }
    required public Name Name { get; init; }
    required public Contact? Contact { get; init; }
    required public Vehicle? Vehicle { get; init; }
    required public DateOfBirth? DateOfBirth { get; init; }
    required public bool HasImage { get; init; }
    public List<NextOfKin> NextOfKins { get; init; }
}
