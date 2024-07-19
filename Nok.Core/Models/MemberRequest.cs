using Nok.Core.Validators;

namespace Nok.Core.Models;

public record MemberRequest(NameDto Name, ContactDetailsDto Contact, VehicleDto Vehicle, DateOfBirthDto DateOfBirth, AddressDto Address, string? ImageUrl)
    : BaseValidationModel<MemberRequest>, IMember
{
    // Dirty fix using records with bogus; https://github.com/bchavez/Bogus/issues/334
    public MemberRequest() : this(Name: default, Contact: default, Vehicle: default, DateOfBirth: default, Address: default, ImageUrl: default)
    {
    }
};

public record MemberRequestWithId : MemberRequest
{
    public MemberRequestWithId(MemberRequest parent) : base(parent) { }
    public Guid Id { get; init; }
}
