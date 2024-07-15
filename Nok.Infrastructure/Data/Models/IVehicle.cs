namespace Nok.Infrastructure.Data.Models;

public interface IVehicle
{
    string? Colour { get; }
    string? Make { get; }
    string? Model { get; }
    string? Notes { get; }
    string RegistrationNumber { get; }
}
