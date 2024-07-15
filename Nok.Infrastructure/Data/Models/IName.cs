namespace Nok.Infrastructure.Data.Models;

public interface IName
{
    string FirstName { get; set; }
    string? MiddleName { get; set; }
    string Surname { get; set; }
    string? Title { get; set; }
}
