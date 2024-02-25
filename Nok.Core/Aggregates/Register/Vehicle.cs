using Ardalis.SharedKernel;

namespace Nok.Core.Aggregates.Register;

public class Vehicle : ValueObject
{
    public string RegistrationNumber { get; private set; } = string.Empty;
    public string Make { get; private set; } = string.Empty;
    public string? Model { get; private set; } = string.Empty;
    public string? Colour { get; private set; } = string.Empty;
    public string? Notes{ get; private set; } = string.Empty;

    public Vehicle(string registrationNumber, string make, string? model, string? colour, string? notes)
    {
        RegistrationNumber = registrationNumber;
        Make = make;
        Model = model;
        Colour = colour;
        Notes = notes;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RegistrationNumber;
        yield return Make;
        yield return Model ?? string.Empty;
        yield return Colour ?? string.Empty;
        yield return Notes ?? string.Empty;
    }

}
