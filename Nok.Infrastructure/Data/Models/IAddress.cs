namespace Nok.Infrastructure.Data.Models;

public interface IAddress
{
    string Address1 { get; }
    string? Address2 { get; }
    string? Country { get; }
    string Postcode { get; }
    string Town { get; }
}
