using Microsoft.EntityFrameworkCore;

namespace Nok.Infrastructure.Data.Models;

[Owned]
public class Address : IAddress
{
    public Address() { }

    public Address(string address1, string? address2, string town, string postcode, string? country)
    {
        Address1 = address1;
        Address2 = address2;
        Town = town;
        Postcode = postcode;
        Country = country;
    }

    public required string Address1 { get; init; }
    public string? Address2 { get; init; }
    public required string Town { get; init; }
    public required string Postcode { get; init; }
    public string? Country { get; init; }
}
