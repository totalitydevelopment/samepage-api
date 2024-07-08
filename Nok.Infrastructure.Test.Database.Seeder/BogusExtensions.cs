using Bogus;
using Bogus.Extensions.UnitedKingdom;
using Nok.Core.Aggregates.Register;

namespace Nok.Infrastructure.Data.Migrations;

internal static class BogusExtensions
{
    public static Address NokAddress(this Faker faker)
        => new Address(
            faker.Address.StreetAddress(),
            faker.Address.SecondaryAddress(),
            faker.Address.City(),
            faker.Address.ZipCode(),
            faker.Address.CountryOfUnitedKingdom()); // Everyone is in the UK for now

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
