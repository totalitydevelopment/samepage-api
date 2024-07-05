using Ardalis.SharedKernel;

namespace Nok.Core.Aggregates.Register;

public class Member : Person, IAggregateRoot
{
    private List<NextOfKin> _nextOfKins = [];

    private Member()
    {
        // Required by EF
    }

    public Member(Guid id, Name name) : base(id, name)
    {
        _nextOfKins = [];
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
        if (Contact == null)
        {
            Contact = new ContactDetails(email, string.Empty, string.Empty, string.Empty);

            return;
        }

        Contact = new ContactDetails(email, Contact.HomeNumber ?? string.Empty, Contact.WorkNumber ?? string.Empty, Contact.MobileNumber ?? string.Empty);
    }
}