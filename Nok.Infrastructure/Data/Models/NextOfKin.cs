namespace Nok.Infrastructure.Data.Models;

public class NextOfKin : INextOfKin
{
    public Guid Id { get; init; }
    public required Name Name { get; init; }
    public Address? Address { get; init; }
    public DateOfBirth? DateOfBirth { get; init; }
    public required ContactDetails ContactDetails { get; init; }
    public required string Relationship { get; init; }
}
