using Microsoft.EntityFrameworkCore;

namespace Nok.Infrastructure.Data.Models;

[Owned]
public class ContactDetails : IContactDetails
{
    public required string Email { get; init; }
    public string? HomeNumber { get; init; }
    public string? WorkNumber { get; init; }
    public string? MobileNumber { get; init; }
}
