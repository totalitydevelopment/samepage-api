using SamePage.Core.Validators;

namespace SamePage.Core.Models;

public record MemberRequest(NameDto Name, ContactDetailsDto Contact, VehicleDto Vehicle, DateOfBirthDto DateOfBirth, AddressDto Address, string? ImageUrl)
    : BaseValidationModel<MemberRequest>, IMember;

public record MemberRequestWithId : MemberRequest
{
    public MemberRequestWithId(MemberRequest parent) : base(parent) { }
    public Guid Id { get; init; }
}
