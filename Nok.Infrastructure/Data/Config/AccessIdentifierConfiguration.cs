using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nok.Core.Aggregates.Register;

namespace Nok.Infrastructure.Data.Config;

public class AccessIdentifierConfiguration : IEntityTypeConfiguration<AccessIdentifier>
{
    public void Configure(EntityTypeBuilder<AccessIdentifier> modelBuilder)
    {
        modelBuilder.HasMany(e => e.Members).WithOne();

        modelBuilder.HasKey(e => e.Id).IsClustered(false);
        modelBuilder.HasIndex(e => e.ClusterId).IsUnique().IsClustered();
        modelBuilder.Property(b => b.ClusterId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        modelBuilder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
        modelBuilder.Property(x => x.Id).ValueGeneratedNever();
        modelBuilder.Property(x => x.AzureOid).ValueGeneratedNever();

        modelBuilder.ToTable("AccessIdentifiers");
    }
}
