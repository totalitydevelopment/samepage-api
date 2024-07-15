using Microsoft.EntityFrameworkCore;

namespace Nok.Infrastructure.Data.Models;

[Owned]
public class Vehicle : IVehicle
{
    public required string RegistrationNumber { get; set; }
    public string? Make { get; init; }
    public string? Model { get; init; }
    public string? Colour { get; init; }
    public string? Notes { get; init; }
}
