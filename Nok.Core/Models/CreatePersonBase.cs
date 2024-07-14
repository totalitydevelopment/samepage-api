namespace Nok.Core.Models;

public class CreatePersonBase
{
    public string? Title { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}
