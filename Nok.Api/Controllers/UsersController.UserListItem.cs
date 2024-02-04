namespace Nok.Api.Controllers;

public class UserListItem
{
    required public Guid UserId { get; init; }
    required public string FirstName { get; init; }
    required public string LastName { get; init; }
    required public DateOnly? DateOfBirth { get; init; }
    required public string? KnownTown { get; init; }
    required public string? KnownVehicleReg { get; init; }
    required public bool HasImage { get; init; }
}
