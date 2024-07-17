using Bogus;
using Nok.Core.Models;

namespace Nok.Api.Controllers.Test;

public class SeedDataGenerator
{
    public IEnumerable<MemberDto> Members { get; } = [];

    public SeedDataGenerator()
    {
        Members = GenerateMembers(amount: 1000);
    }

    private static IEnumerable<MemberDto> GenerateMembers(int amount)
    {
        var memberFaker = new Faker<MemberDto>("en_GB")
            .RuleFor(x => x.Id, f => f.Random.Uuid())
            .RuleFor(x => x.Name, f => f.NokName())
            .RuleFor(x => x.Address, f => f.NokAddress())
            .RuleFor(x => x.Contact, f => f.NokContactDetails())
            .RuleFor(x => x.DateOfBirth, f => f.NokDateOfBirth())
            .RuleFor(x => x.Vehicle, f => f.NokVehicle());

        var members = Enumerable.Range(1, amount)
            .Select(i => SeedRow(memberFaker, i))
            .ToList();

        return members;
    }

    private static T SeedRow<T>(Faker<T> faker, int rowId) where T : class =>
        faker.UseSeed(rowId).Generate();
}
