using Nok.Core.Validators;

namespace Nok.Core.Models;

public record NextOfKinRequest(NameDto Name, ContactDetailsDto Contact, AddressDto Address, string Relationship) : BaseValidationModel<NextOfKinRequest>, INextOfKin;

public record NextOfKinRequestWithId : NextOfKinRequest
{
    public NextOfKinRequestWithId(NextOfKinRequest parent) : base(parent) { }
    public Guid Id { get; init; }
}
