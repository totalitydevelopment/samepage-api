namespace Nok.Core.Models;

public class CreateMember
{
    public CreateMember(string? title, string firstName, string? middleName, string lastName, string email)
    {
        Title = title;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        Email = email;
    }

    public string? Title { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string? MiddleName { get; private set; }
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
}
