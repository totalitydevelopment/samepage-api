namespace SamePage.Core.Aggregates.Register;

public class Member : Person
{
    public DateOfBirth? DateOfBirth { get; private set; }
    public Vehicle? Vehicle { get; private set; }
    public bool HasImage => !string.IsNullOrWhiteSpace(ImageUrl);
    public string? ImageUrl { get; private set; }
    public string? NationalInsuranceNumber { get; private set; }

    public virtual IList<NextOfKin> NextOfKin { get; init; } = [];

    public void SetDateOfBirth(DateOfBirth dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void SetVehicle(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public void SetContactEmail(string email)
    {
        Contact = new ContactDetails(email, Contact);
    }
}
