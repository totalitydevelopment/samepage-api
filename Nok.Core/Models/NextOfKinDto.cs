namespace Nok.Core.Models;

public record NextOfKinDto(Guid Id, NameDto Name, ContactDetailsDto Contact, AddressDto Address, string Relationship);
