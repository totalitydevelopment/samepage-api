using Nok.Core.Enums;
using Nok.Core.Extensions;

namespace Nok.Core.Aggregates.Register;

public class Member : Person
{
    private readonly List<NextOfKin> _nextOfKins = [];

    private Member()
    {
        // Required by EF
    }

    public Member(Guid id, Name name, Guid accessIdentifiedId) : base(id, name, accessIdentifiedId)
    {

    }

    public DateOfBirth? DateOfBirth { get; private set; }
    public Vehicle? Vehicle { get; private set; }
    public bool HasImage => !string.IsNullOrWhiteSpace(ImageUrl);
    public string? ImageUrl { get; private set; }
    public string? NationalInsuranceNumber { get; private set; }

    public IReadOnlyList<NextOfKin> NextOfKins => _nextOfKins.AsReadOnly();

    public void SetDateOfBirth(DateOfBirth dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void SetVehicle(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public void SetNextOfKin(NextOfKin nextOfKin)
    {
        _nextOfKins.Add(nextOfKin);
    }

    public void SetContactEmail(string email)
    {
        Contact = new ContactDetails(email, Contact);
    }
}

public class AccessIdentifier : GuidDataEntity
{
    private readonly List<Member> _members = [];

    public required Guid AzureOid { get; init; }
    public required AccessIdentifierType Type { get; init; }
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    private AccessIdentifier()
    {
        // Required by EF
    }

    public AccessIdentifier(Guid id, Guid azureOid, AccessIdentifierType type)
    {
        Id = id;
        AzureOid = azureOid;
        Type = type;

        CreatedBy = Id;
        CreatedDate = SystemTime.UtcNow();
        UpdatedBy = Id;
        UpdatedDate = SystemTime.UtcNow();
    }

    public void AddMember(Member nextOfKin)
    {
        _members.Add(nextOfKin);
    }
}
