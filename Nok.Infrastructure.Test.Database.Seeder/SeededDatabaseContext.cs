using Microsoft.EntityFrameworkCore;
using Nok.Core.Aggregates.Register;
using Nok.Infrastructure.Data;

namespace Nok.Infrastructure.Test.Database.Seeder;

public class SeededDatabaseContext : DatabaseContext
{
    private readonly SeedDataGenerator _seedDataGenerator;

    public SeededDatabaseContext(DbContextOptions<DatabaseContext> options, SeedDataGenerator seedDataGenerator) : base(options)
    {
        _seedDataGenerator = seedDataGenerator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Member>().HasData(_seedDataGenerator.Members);
    }
}
