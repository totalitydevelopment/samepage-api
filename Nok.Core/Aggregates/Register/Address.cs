using Ardalis.SharedKernel;

namespace Nok.Core.Aggregates.Register;

public class Address : ValueObject
{
    public string Address1 { get; private set; } = string.Empty;
    public string? Address2 { get; private set; } = string.Empty;
    public string Town { get; private set; } = string.Empty;
    public string Postcode { get; private set; } = string.Empty;
    public string? Country { get; private set; } = string.Empty;

    public Address(string address1, string? address2, string town, string postcode, string? country)
    {
        Address1 = address1;
        Address2 = address2;
        Town = town;
        Postcode = postcode;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address1;
        yield return Address2 ?? string.Empty;
        yield return Town;
        yield return Postcode;
        yield return Country ?? string.Empty;
    }
}
