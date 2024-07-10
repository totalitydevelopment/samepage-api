using Microsoft.EntityFrameworkCore;
using Nok.Infrastructure.Data;
using Nok.Infrastructure.Test.Database.Seeder;
using Nok.Core.Extensions;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
        });

        builder.Services.AddSingleton<SeedDataGenerator>();
        builder.Services.AddSingleton<SeededDatabaseContext>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsLocal())
        {
            var seeder = app.Services.GetRequiredService<SeededDatabaseContext>();
        }

        app.Run();
    }
}
