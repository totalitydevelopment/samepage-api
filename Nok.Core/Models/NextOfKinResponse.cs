namespace Nok.Core.Models;

public record NextOfKinResponse(Guid Id, NameDto Name, ContactDetailsDto Contact, AddressDto Address, string Relationship) : INextOfKin;
