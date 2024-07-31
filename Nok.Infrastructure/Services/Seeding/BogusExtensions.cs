using Bogus;
using Bogus.Extensions.UnitedKingdom;
using SamePage.Core.Aggregates.Register;

namespace SamePage.Infrastructure.Services.Seeding;

internal static class BogusExtensions
{
    public static Name NokName(this Faker faker)
    {
        var person = faker.Person;
        var title = person.Gender == Bogus.DataSets.Name.Gender.Male ? "Mr" : "Miss"; // very basic

        return new Name()
        {
            Title = title,
            FirstName = person.FirstName,
            MiddleName = null,
            Surname = person.LastName
        };
    }

    public static Address NokAddress(this Faker faker) =>
        new Address()
        {
            Address1 = faker.Address.StreetAddress(),
            Address2 = faker.Address.SecondaryAddress(),
            Town = faker.Address.City(),
            Postcode = faker.Address.ZipCode(),
            Country = faker.Address.CountryCode()
        };

    public static ContactDetails NokContactDetails(this Faker faker) =>
        new ContactDetails(faker.Person.Email)
        {
            MobileNumber = faker.Person.Phone
        };


    public static DateOfBirth NokDateOfBirth(this Faker faker) =>
        new DateOfBirth()
        {
            Year = faker.Person.DateOfBirth.Year,
            Month = faker.Person.DateOfBirth.Month,
            Day = faker.Person.DateOfBirth.Day
        };

    public static Vehicle NokVehicle(this Faker faker) =>
        new Vehicle()
        {
            RegistrationNumber = faker.Vehicle.GbRegistrationPlate(new DateTime(2001, 09, 01), new DateTime(2024, 1, 1)),
            Make = faker.Vehicle.Manufacturer(),
            Model = faker.Vehicle.Model(),
            Colour = faker.Commerce.Color(),
            Notes = faker.Commerce.ProductDescription()
        };
}
