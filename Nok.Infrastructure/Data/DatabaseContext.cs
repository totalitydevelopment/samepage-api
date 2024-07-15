using Microsoft.EntityFrameworkCore;
using Nok.Infrastructure.Data.Models;

namespace Nok.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<AccessIdentifier> AccessIdentifiers { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<NextOfKin> NextOfKin { get; set; }
}
