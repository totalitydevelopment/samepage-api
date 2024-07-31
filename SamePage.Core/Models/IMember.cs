namespace SamePage.Core.Models;

public interface IMember
{
    NameDto Name { get; }
    ContactDetailsDto Contact { get; }
    VehicleDto Vehicle { get; }
    DateOfBirthDto DateOfBirth { get; }
    AddressDto Address { get; }
    string? ImageUrl { get; }
}
