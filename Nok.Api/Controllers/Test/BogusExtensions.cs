using Bogus;
using Bogus.Extensions.UnitedKingdom;
using Nok.Core.Aggregates.Register;

namespace Nok.Infrastructure.Data.Migrations;

internal static class BogusExtensions
{
    public static Name NokName(this Faker faker)
    {
        var person = faker.Person;
        var title = person.Gender == Bogus.DataSets.Name.Gender.Male ? "Mr" : "Miss"; // very basic

        return new Name(title, person.FirstName, null, person.LastName);
    }

    public static Address NokAddress(this Faker faker)
        => new Address(
            faker.Address.StreetAddress(),
            faker.Address.SecondaryAddress(),
            faker.Address.City(),
            faker.Address.ZipCode(),
            faker.Address.CountryCode());

    public static ContactDetails NokContactDetails(this Faker faker)
        => new ContactDetails(
            faker.Person.Email,
            faker.Person.Phone,
            faker.Person.Phone,
            faker.Person.Phone);

    public static DateOfBirth NokDateOfBirth(this Faker faker)
    {
        var dob = faker.Person.DateOfBirth;

        return new DateOfBirth(dob.Year, dob.Month, dob.Day);
    }

    public static Vehicle NokVehicle(this Faker faker)
        => new Vehicle(
            faker.Vehicle.GbRegistrationPlate(new DateTime(2001, 09, 01), new DateTime(2024, 1, 1)),
            faker.Vehicle.Manufacturer(),
            faker.Vehicle.Model(),
            faker.Commerce.Color(),
            faker.Commerce.ProductDescription());


}
