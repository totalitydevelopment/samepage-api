using Ardalis.SharedKernel;
using Nok.Core.Extensions;

namespace Nok.Core.Aggregates.Register;

public class Person : GuidDataEntity
{
    public Name Name { get; protected set; }
    public ContactDetails? Contact { get; protected set; }
    public Address? Address { get; protected set; }

    protected Person()
    {
        // Required by EF
    }

    public Person(Guid id, Name name)
    {
        Id = id;
        Name = name;

        CreatedBy = Id;
        CreatedDate = SystemTime.UtcNow();
        UpdatedBy = Id;
        UpdatedDate = SystemTime.UtcNow();
    }

    public void SetContactDetails(ContactDetails contactDetails)
    {
        Contact = contactDetails;
    }

    public void SetAddress(Address address)
    {
        Address = address;
    }
}

public class Member : Person, IAggregateRoot
{
    private List<NextOfKin> _nextOfKins = [];

    private Member()
    {
        // Required by EF
    }

    public Member(Guid id, Name name): base(id, name)
    {
        _nextOfKins = [];
    }

   
    public DateOfBirth? DateOfBirth { get; private set; }
    public Vehicle? Vehicle { get; private set; }
    public bool HasImage => !string.IsNullOrWhiteSpace(ImageUrl);
    public string? ImageUrl { get; private set; }
    public string? NationalInsuranceNumber { get; private set; }

    public IReadOnlyList<NextOfKin> NextOfKins => _nextOfKins.AsReadOnly();

    public void SetDateOfBirth(DateOfBirth dateOfBirth)
    {
        DateOfBirth = dateOfBirth;
    }

    public void SetVehicle(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }

    public void SetNextOfKin(NextOfKin nextOfKin)
    {
        _nextOfKins.Add(nextOfKin);
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

public class NextOfKin : Person
{
    private NextOfKin()
    {
        // Required by EF
    }

    public NextOfKin(Guid id, Name name, ContactDetails contactDetails, string relationship) : base(id, name)
    {
        Relationship = relationship;
        Contact = contactDetails;
    }

    public string Relationship { get; init; }
}

public class Address : ValueObject
{
    public string Address1 { get; private set; } = string.Empty;
    public string? Address2 { get; private set; } = string.Empty;
    public string Town { get; private set; } = string.Empty;
    public string Postcode { get; private set; } = string.Empty;
    public string? Country { get; private set; } = string.Empty;

    public Address(string address1, string? address2, string town, string postcode, string? country)
    {
        Address1 = address1;
        Address2 = address2;
        Town = town;
        Postcode = postcode;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address1;
        yield return Address2 ?? string.Empty;
        yield return Town;
        yield return Postcode;
        yield return Country ?? string.Empty;
    }
}

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

public class Vehicle : ValueObject
{
    public string RegistrationNumber { get; private set; } = string.Empty;
    public string Make { get; private set; } = string.Empty;
    public string? Model { get; private set; } = string.Empty;
    public string? Colour { get; private set; } = string.Empty;
    public string? Notes{ get; private set; } = string.Empty;

    public Vehicle(string registrationNumber, string make, string? model, string? colour, string? notes)
    {
        RegistrationNumber = registrationNumber;
        Make = make;
        Model = model;
        Colour = colour;
        Notes = notes;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return RegistrationNumber;
        yield return Make;
        yield return Model ?? string.Empty;
        yield return Colour ?? string.Empty;
        yield return Notes ?? string.Empty;
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
