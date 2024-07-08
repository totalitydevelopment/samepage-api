namespace Nok.Core.Aggregates.Register;

public record DateOfBirth(int Year, int? Month, int? Day)
{
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
