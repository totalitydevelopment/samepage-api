using Bogus;
using Bogus.Extensions.UnitedKingdom;
using Nok.Infrastructure.Data.Models;

namespace Nok.Api.Controllers.Test;

internal static class BogusExtensions
{
    public static IName NokName(this Faker faker)
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

    public static IAddress NokAddress(this Faker faker)
        => new Address()
        {
            Address1 = faker.Address.StreetAddress(),
            Address2 = faker.Address.SecondaryAddress(),
            Town = faker.Address.City(),
            Postcode = faker.Address.ZipCode(),
            Country = faker.Address.CountryCode()
        };

    public static IContactDetails NokContactDetails(this Faker faker)
        => new ContactDetails()
        {
            Email = faker.Person.Email,
            HomeNumber = faker.Person.Phone,
            MobileNumber = faker.Person.Phone,
            WorkNumber = faker.Person.Phone
        };

    public static IDateOfBirth NokDateOfBirth(this Faker faker)
        => new DateOfBirth(faker.Person.DateOfBirth);

    public static IVehicle NokVehicle(this Faker faker)
        => new Vehicle()
        {
            RegistrationNumber = faker.Vehicle.GbRegistrationPlate(new DateTime(2001, 09, 01), new DateTime(2024, 1, 1)),
            Make = faker.Vehicle.Manufacturer(),
            Model = faker.Vehicle.Model(),
            Colour = faker.Commerce.Color(),
            Notes = faker.Commerce.ProductDescription()
        };


}
