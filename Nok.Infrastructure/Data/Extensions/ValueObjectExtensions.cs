//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using Nok.Core.Aggregates.Register;

//namespace Nok.Infrastructure.Data.Extensions
//{
//    public static class ValueObjectExtensions
//    {
//        public static void ConfigureNameValueObject(this OwnedNavigationBuilder<Person> builder)
//        {
//            builder.Property(pp => pp.Title).HasColumnName("Name_Title").IsRequired(false).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
//            builder.Property(pp => pp.FirstName).HasColumnName("Name_FirstName").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
//            builder.Property(pp => pp.MiddleName).HasColumnName("Name_MiddleName").IsRequired(false).HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
//            builder.Property(pp => pp.Surname).HasColumnName("Name_Surname").HasMaxLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
//        }

//        public static void ConfigureContactDetailsValueObject(this OwnedNavigationBuilder<Person> builder)
//        {
//            builder.Property(pp => pp.Email).HasColumnName("Contact_Email").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_EMAIL_LENGTH);
//            builder.Property(pp => pp.HomeNumber).HasColumnName("Contact_HomeNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
//            builder.Property(pp => pp.WorkNumber).HasColumnName("Contact_WorkNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
//            builder.Property(pp => pp.MobileNumber).HasColumnName("Contact_MobileNumber").IsRequired(true).HasMaxLength(DataSchemaConstants.DEFAULT_TEL_LENGTH);
//        }

//        public static void ConfigureAddressValueObject(this OwnedNavigationBuilder<Person> builder)
//        {
//            builder.Property(pp => pp.Address1).HasColumnName("Address_Address1").HasMaxLength(200);
//            builder.Property(pp => pp.Address2).HasColumnName("Address_Address2").HasMaxLength(100);
//            builder.Property(pp => pp.Town).HasColumnName("Address_City").HasMaxLength(200);
//            builder.Property(pp => pp.Postcode).HasColumnName("Address_Town").HasMaxLength(200);
//            builder.Property(pp => pp.Country).HasColumnName("Address_Country").HasMaxLength(10);
//        }

//        public static void ConfigureDateOfBirthValueObject(this OwnedNavigationBuilder<Member> builder)
//        {
//            // Configure DateOfBirth properties if needed
//        }

//        public static void ConfigureVehicleValueObject(this OwnedNavigationBuilder<Member> builder)
//        {
//            builder.Property(pp => pp.RegistrationNumber).HasColumnName("Vehicle_Registration").HasMaxLength(20);
//            builder.Property(pp => pp.Make).HasColumnName("Vehicle_Make").HasMaxLength(100);
//            builder.Property(pp => pp.Model).HasColumnName("Vehicle_Model").HasMaxLength(100);
//            builder.Property(pp => pp.Colour).HasColumnName("Vehicle_Colour").HasMaxLength(50);
//            builder.Property(pp => pp.Notes).HasColumnName("Vehicle_Notes").HasMaxLength(1000);
//        }
//    }
//}
