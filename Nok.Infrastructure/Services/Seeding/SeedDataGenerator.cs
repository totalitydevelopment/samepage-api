using Bogus;
using Nok.Core.Aggregates.Register;

namespace Nok.Infrastructure.Services.Seeding;

public class SeedDataGenerator
{
    public IEnumerable<Member> Members { get; } = [];

    public SeedDataGenerator()
    {
        Members = GenerateMembers(amount: 1000);
    }

    private static int _memberSeed = 0;

    private static IEnumerable<Member> GenerateMembers(int amount)
    {
        var memberFaker = new Faker<Member>("en_GB")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Name, f => f.NokName())
            .RuleFor(x => x.Address, f => f.NokAddress())
            .RuleFor(x => x.Contact, f => f.NokContactDetails())
            .RuleFor(x => x.DateOfBirth, f => f.NokDateOfBirth())
            .RuleFor(x => x.Vehicle, f => f.NokVehicle())
            .RuleFor(x => x.NextOfKin, f => GenerateNextOfKin(f.Random.Number(3)));

        var members = Enumerable.Range(1, amount)
            .Select(i => SeedRow(memberFaker, i))
            .ToList();

        return members;
    }

    private static IEnumerable<NextOfKin> GenerateNextOfKin(int amount)
    {
        var nextOfKinFaker = new Faker<NextOfKin>("en_GB")
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Name, f => f.NokName())
            .RuleFor(x => x.Address, f => f.NokAddress())
            .RuleFor(x => x.Contact, f => f.NokContactDetails())
            .RuleFor(x => x.Relationship, f => ((Relationship)f.Random.Number(4)).ToString());

        // Unique seed per member's NextOfKin collection
        var seed = 10000000 * Interlocked.Increment(ref _memberSeed);

        var nextOfKin = Enumerable.Range(1, amount)
            .Select(i => SeedRow(nextOfKinFaker, i + seed))
            .ToList();

        return nextOfKin;
    }

    private static T SeedRow<T>(Faker<T> faker, int seed) where T : class =>
        faker.UseSeed(seed).Generate();

    private enum Relationship
    {
        Parent,
        Sibling,
        Friend,
        Carer,
        Guardian,
    }
}
