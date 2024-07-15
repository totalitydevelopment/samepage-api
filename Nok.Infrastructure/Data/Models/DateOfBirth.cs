using Microsoft.EntityFrameworkCore;

namespace Nok.Infrastructure.Data.Models;

[Owned]
public class DateOfBirth : IDateOfBirth
{
    public DateOfBirth() { }

    public DateOfBirth(DateTime dateOfBirth)
    {
        Year = dateOfBirth.Year;
        Month = dateOfBirth.Month;
        Day = dateOfBirth.Day;
    }

    public int Year { get; set; }
    public int? Month { get; set; }
    public int? Day { get; set; }
}
