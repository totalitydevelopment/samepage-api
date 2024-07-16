namespace Nok.Core.Aggregates.Register;

public class Name
{
    public string? Title { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string Surname { get; set; }
}
