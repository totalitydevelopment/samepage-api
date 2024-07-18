using Bogus;
using Bogus.Extensions.UnitedKingdom;
using Nok.Core.Models;

namespace Nok.Api.Controllers.Test;

internal static class BogusExtensions
{
    public static NameDto NokName(this Faker faker)
    {
        var person = faker.Person;
        var title = person.Gender == Bogus.DataSets.Name.Gender.Male ? "Mr" : "Miss"; // very basic

        return new NameDto(title, person.FirstName, null, person.LastName);
    }

    public static AddressDto NokAddress(this Faker faker) =>
        new AddressDto(
            faker.Address.StreetAddress(),
            faker.Address.SecondaryAddress(),
            faker.Address.City(),
            faker.Address.ZipCode(),
            faker.Address.CountryCode());

    public static ContactDetailsDto NokContactDetails(this Faker faker) =>
        new ContactDetailsDto(
            faker.Person.Email,
            faker.Person.Phone,
            faker.Person.Phone,
            faker.Person.Phone);


    public static DateOfBirthDto NokDateOfBirth(this Faker faker) =>
        new DateOfBirthDto(
            faker.Person.DateOfBirth.Year,
            faker.Person.DateOfBirth.Month,
            faker.Person.DateOfBirth.Day);

    public static VehicleDto NokVehicle(this Faker faker) =>
        new VehicleDto(
            faker.Vehicle.GbRegistrationPlate(new DateTime(2001, 09, 01), new DateTime(2024, 1, 1)),
            faker.Vehicle.Manufacturer(),
            faker.Vehicle.Model(),
            faker.Commerce.Color(),
            faker.Commerce.ProductDescription());
}
