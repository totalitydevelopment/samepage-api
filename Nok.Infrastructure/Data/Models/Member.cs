using System.ComponentModel.DataAnnotations;

namespace Nok.Infrastructure.Data.Models;

public class Member : IMember
{
    [Key]
    public required Guid Id { get; init; }
    public required Name Name { get; init; }
    public required Address Address { get; init; }
    public required ContactDetails ContactDetails { get; init; }
    public required DateOfBirth DateOfBirth { get; init; }
    public required Vehicle Vehicle { get; init; }
    public string? ImageUrl { get; init; }
    public virtual IList<NextOfKin> NextOfKin { get; init; } = [];
}
