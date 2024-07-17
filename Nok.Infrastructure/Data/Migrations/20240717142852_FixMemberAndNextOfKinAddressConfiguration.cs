using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMemberAndNextOfKinAddressConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "NextOfKins",
                newName: "Address_Postcode");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "Members",
                newName: "Address_Postcode");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "NextOfKins",
                type: "nvarchar(56)",
                maxLength: 56,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "Members",
                type: "nvarchar(56)",
                maxLength: 56,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Postcode",
                table: "NextOfKins",
                newName: "Address_City");

            migrationBuilder.RenameColumn(
                name: "Address_Postcode",
                table: "Members",
                newName: "Address_City");

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "NextOfKins",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(56)",
                oldMaxLength: 56,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address_Country",
                table: "Members",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(56)",
                oldMaxLength: 56,
                oldNullable: true);
        }
    }
}
