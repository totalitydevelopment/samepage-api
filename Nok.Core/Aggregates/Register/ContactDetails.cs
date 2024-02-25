using Ardalis.SharedKernel;

namespace Nok.Core.Aggregates.Register;

public class ContactDetails : ValueObject
{
    public string Email { get; private set; } = string.Empty;
    public string? HomeNumber { get; private set; } = string.Empty;
    public string? WorkNumber { get; private set; } = string.Empty;
    public string? MobileNumber { get; private set; } = string.Empty;

    public ContactDetails(string email, string? homeNumber, string? workNumber, string? mobileNumber)
    {
        Email = email;
        HomeNumber = homeNumber;
        WorkNumber = workNumber;
        MobileNumber = mobileNumber;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return HomeNumber ?? string.Empty;
        yield return WorkNumber ?? string.Empty;
    }
}
