using Nok.Core.Aggregates.Register;

namespace Nok.Core.Models;

public class GetNok : BaseId
{
    required public string? Relationship { get; init; }
    required public Name Name { get; init; }
}
