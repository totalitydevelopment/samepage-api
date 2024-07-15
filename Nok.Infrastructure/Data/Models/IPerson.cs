namespace Nok.Infrastructure.Data.Models;

public interface IPerson
{
    Guid Id { get; }
    Name Name { get; }
    Address? Address { get; }
    ContactDetails ContactDetails { get; }
    DateOfBirth? DateOfBirth { get; }
}
