using Nok.Infrastructure.Data.Models;

namespace Nok.Api.Controllers;

public class CreateNextOfKinRequest
{
    public required Name Name { get; init; }
    public Address? Address { get; init; }
    public DateOfBirth? DateOfBirth { get; init; }
    public required ContactDetails ContactDetails { get; init; }
    public required string Relationship { get; init; }
}
