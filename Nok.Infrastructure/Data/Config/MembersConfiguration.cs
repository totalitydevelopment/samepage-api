using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nok.Core.Aggregates.Register;

namespace Nok.Infrastructure.Data.Config;

public class MembersConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> modelBuilder)
    {
        modelBuilder.OwnsOne(p => p.Name, p => {
            p.Property(pp => pp.Title).HasColumnName("Name_Title").IsRequired(false).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
            p.Property(pp => pp.FirstName).HasColumnName("Name_FirstName").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
            p.Property(pp => pp.MiddleName).HasColumnName("Name_MiddleName").IsRequired(false).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
            p.Property(pp => pp.Surname).HasColumnName("Name_Surname").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
        });

        //modelBuilder.Property(p => p.Contact).IsRequired(false);

        modelBuilder.OwnsOne(p => p.Contact, p => {
            p.Property(pp => pp.Email).HasColumnName("Contact_Email").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_EMAIL_LENGTH);
           p.Property(pp => pp.HomeNumber).HasColumnName("Contact_HomeNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
           p.Property(pp => pp.WorkNumber).HasColumnName("Contact_WorkNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
           p.Property(pp => pp.MobileNumber).HasColumnName("Contact_MobileNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
        });

        modelBuilder.OwnsOne(builder => builder.DateOfBirth);

        modelBuilder.HasKey(e => e.Id).IsClustered(false);
        modelBuilder.HasIndex(e => e.ClusterId).IsUnique().IsClustered();
        modelBuilder.Property(b => b.ClusterId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        modelBuilder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
        modelBuilder.Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.ToTable("Members", DatabaseSchemaNames.Member);
    }
}
