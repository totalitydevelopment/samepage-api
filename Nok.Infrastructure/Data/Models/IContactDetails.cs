namespace Nok.Infrastructure.Data.Models;

public interface IContactDetails
{
    string Email { get; }
    string? HomeNumber { get; }
    string? MobileNumber { get; }
    string? WorkNumber { get; }
}
