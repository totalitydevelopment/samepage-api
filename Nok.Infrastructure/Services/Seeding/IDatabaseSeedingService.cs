namespace Nok.Infrastructure.Services.Seeding;

public interface IDatabaseSeedingService
{
    Task SeedDatabase(Guid accessIdentifierId);
}
