namespace Nok.Infrastructure.Data.Models;

public interface IDateOfBirth
{
    int? Day { get; }
    int? Month { get; }
    int Year { get; }
}
