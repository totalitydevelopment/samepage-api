
using Nok.Infrastructure.Data;

namespace Nok.Infrastructure.Services.Seeding;

public class DatabaseSeedingService : IDatabaseSeedingService
{
    private static readonly SeedDataGenerator SeedDataGenerator = new SeedDataGenerator();

    private readonly DatabaseContext _databaseContext;

    public DatabaseSeedingService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task SeedDatabase(Guid accessIdentifierId)
    {
        var accessIdentifier = await _databaseContext.GetAccessIdentifierAsync(accessIdentifierId);

        foreach (var member in SeedDataGenerator.Members)
        {
            accessIdentifier.Members.Add(member);
        }

        _databaseContext.SaveChanges();
    }
}
