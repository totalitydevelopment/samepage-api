using Microsoft.EntityFrameworkCore;

namespace Nok.Infrastructure.Data.Models;

[Owned]
public class Name : IName
{
    public string? Title { get; set; }
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string Surname { get; set; }
}
