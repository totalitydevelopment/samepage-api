namespace Nok.Core.Aggregates.Register;

public class ContactDetails
{
    public required string Email { get; init; }
    public string? HomeNumber { get; init; }
    public string? WorkNumber { get; init; }
    public string? MobileNumber { get; init; }
}
