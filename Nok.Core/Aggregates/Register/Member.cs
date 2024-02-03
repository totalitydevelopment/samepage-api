using Ardalis.SharedKernel;
using Nok.Core.Extensions;

namespace Nok.Core.Aggregates.Register;

public class Member : GuidDataEntity, IAggregateRoot
{
    private Member()
    {
        // Required by EF
    }

    public Member(Guid id, Name name)
    {
        Id = id;
        Name = name;

        CreatedBy = Id;
        CreatedDate = SystemTime.UtcNow();
        UpdatedBy = Id;
        UpdatedDate = SystemTime.UtcNow();
    }

    public Name Name { get; private set; }
    public ContactDetails? Contact { get; private set; }
    public DateOfBirth? DateOfBirth { get; private set; }

    public void SetContactDetails(ContactDetails contactDetails)
    {
        Contact = contactDetails;
    }

    public void SetDateOfBirth(DateOfBirth dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void SetContactEmail(string email)
    {

        if (Contact == null)
        {
            Contact = new ContactDetails(email, string.Empty, string.Empty, string.Empty);

            return;
        }

        Contact = new ContactDetails(email, Contact.HomeNumber ?? string.Empty, Contact.WorkNumber ?? string.Empty, Contact.MobileNumber ?? string.Empty);
    }
}


public class DateOfBirth : ValueObject
{
    public DateTime Value { get; private set; }

    public DateOfBirth(DateTime value)
    {
        if (value > SystemTime.UtcNow())
        {
            throw new ArgumentException("Date of birth cannot be in the future", nameof(value));
        }

        Value = value;
    }

    public int Age
    {
        get
        {
            var age = DateTime.UtcNow.Year - Value.Year;

            if (DateTime.UtcNow < Value.AddYears(age)) age--;

            return age;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

public class Vehicle : ValueObject
{
    public string RegistrationNumber { get; private set; } = string.Empty;
    public string Make { get; private set; } = string.Empty;
    public string Model { get; private set; } = string.Empty;

    public Vehicle(string registrationNumber, string make, string model)
    {
        RegistrationNumber = registrationNumber;
        Make = make;
        Model = model;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RegistrationNumber;
        yield return Make;
        yield return Model;
    }
}

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

public class ContactDetails : ValueObject
{
    public string Email { get; private set; } = string.Empty;
    public string? HomeNumber { get; private set; } = string.Empty;
    public string? WorkNumber { get; private set; } = string.Empty;
    public string? MobileNumber { get; private set; } = string.Empty;

    public ContactDetails(string email, string? homeNumber, string? workNumber, string? mobileNumber)
    {
        Email = email;
        HomeNumber = homeNumber;
        WorkNumber = workNumber;
        MobileNumber = mobileNumber;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return HomeNumber ?? string.Empty;
        yield return WorkNumber ?? string.Empty;
    }
}
