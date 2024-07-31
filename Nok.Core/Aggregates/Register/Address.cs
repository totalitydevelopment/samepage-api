namespace SamePage.Core.Aggregates.Register;

public class Address
{
    public required string Address1 { get; init; }
    public string? Address2 { get; init; }
    public required string Town { get; init; }
    public required string Postcode { get; init; }
    public string? Country { get; init; }
}
