
namespace Nok.Infrastructure.Data.Models;

public interface IMember : IPerson
{
    Vehicle Vehicle { get; init; }
    string? ImageUrl { get; init; }
    IList<NextOfKin> NextOfKin { get; init; }
}
