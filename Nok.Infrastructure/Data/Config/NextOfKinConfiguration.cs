using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nok.Core.Aggregates.Register;

namespace Nok.Infrastructure.Data.Config;

public class NextOfKinConfiguration : IEntityTypeConfiguration<NextOfKin>
{
    public void Configure(EntityTypeBuilder<NextOfKin> modelBuilder)
    {
        modelBuilder.OwnsOne(p => p.Name, p =>
        {
            p.Property(pp => pp.Title).HasColumnName("Name_Title").IsRequired(false).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
            p.Property(pp => pp.FirstName).HasColumnName("Name_FirstName").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
            p.Property(pp => pp.MiddleName).HasColumnName("Name_MiddleName").IsRequired(false).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
            p.Property(pp => pp.Surname).HasColumnName("Name_Surname").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        });

        modelBuilder.OwnsOne(p => p.Contact, p =>
        {
            p.Property(pp => pp.Email).HasColumnName("Contact_Email").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_EMAIL_LENGTH);
            p.Property(pp => pp.HomeNumber).HasColumnName("Contact_HomeNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
            p.Property(pp => pp.WorkNumber).HasColumnName("Contact_WorkNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
            p.Property(pp => pp.MobileNumber).HasColumnName("Contact_MobileNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
        });

        modelBuilder.OwnsOne(p => p.Address, p =>
        {
            p.Property(pp => pp.Address1).HasColumnName("Address_Address1").HasMaxLength(200);
            p.Property(pp => pp.Address2).HasColumnName("Address_Address2").HasMaxLength(100);
            p.Property(pp => pp.Town).HasColumnName("Address_City").HasMaxLength(200);
            p.Property(pp => pp.Postcode).HasColumnName("Address_Town").HasMaxLength(200);
            p.Property(pp => pp.Country).HasColumnName("Address_Country").HasMaxLength(10);
        });

        modelBuilder.Property(e => e.Relationship).HasMaxLength(100);

        modelBuilder.HasKey(e => e.Id).IsClustered(false);
        modelBuilder.HasIndex(e => e.ClusterId).IsUnique().IsClustered();
        modelBuilder.Property(b => b.ClusterId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        modelBuilder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
        modelBuilder.Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.ToTable("NextOfKins", DatabaseSchemaNames.Member);
    }
}
