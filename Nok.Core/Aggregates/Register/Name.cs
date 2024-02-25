using Ardalis.SharedKernel;

namespace Nok.Core.Aggregates.Register;

public class Name : ValueObject
{
    public string? Title { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string? MiddleName { get; private set; } = string.Empty;
    public string Surname { get; private set; } = string.Empty;

    public Name(string? title, string firstName, string? middleName, string surname)
    {
        Title = title;
        FirstName = firstName;
        MiddleName = middleName;
        Surname = surname;
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Title;
        yield return FirstName;
        yield return MiddleName ?? string.Empty;
        yield return Surname;
    }
}
