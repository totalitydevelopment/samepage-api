using Ardalis.SharedKernel;

namespace Nok.Core.Aggregates.Register;

public class DateOfBirth : ValueObject
{
    public int Year { get; private set; }
    public int? Month { get; private set; }
    public int? Day { get; private set; }

    public DateOfBirth(int year, int? month, int? day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    // add age calculation if we know the date of birth
    public int? Age
    {
        get
        {
            if (Year == 0)
            {
                return null;
            }

            var today = DateTime.Today;
            var age = today.Year - Year;

            if (Month > today.Month || (Month == today.Month && Day > today.Day))
            {
                age--;
            }

            return age;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Year;
        yield return Month ?? 0;
        yield return Day ?? 0;
    }
}
