namespace Nok.Core.Aggregates.Register;

public class Vehicle
{
    public required string RegistrationNumber { get; set; }
    public string? Make { get; init; }
    public string? Model { get; init; }
    public string? Colour { get; init; }
    public string? Notes { get; init; }
}
