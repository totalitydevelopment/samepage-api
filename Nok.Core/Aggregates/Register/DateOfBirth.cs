namespace Nok.Core.Aggregates.Register;

public class DateOfBirth
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
}
