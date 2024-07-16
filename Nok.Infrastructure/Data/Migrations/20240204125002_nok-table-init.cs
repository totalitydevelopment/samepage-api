using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class noktableinit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateOfBirth_Value",
            schema: "Member",
            table: "Members");

        migrationBuilder.AddColumn<string>(
            name: "Address_Address1",
            schema: "Member",
            table: "Members",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Address_Address2",
            schema: "Member",
            table: "Members",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Address_City",
            schema: "Member",
            table: "Members",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Address_Country",
            schema: "Member",
            table: "Members",
            type: "nvarchar(10)",
            maxLength: 10,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Address_Town",
            schema: "Member",
            table: "Members",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "DateOfBirth_Day",
            schema: "Member",
            table: "Members",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "DateOfBirth_Month",
            schema: "Member",
            table: "Members",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "DateOfBirth_Year",
            schema: "Member",
            table: "Members",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "ImageUrl",
            schema: "Member",
            table: "Members",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "NationalInsuranceNumber",
            schema: "Member",
            table: "Members",
            type: "nvarchar(20)",
            maxLength: 20,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Vehicle_Colour",
            schema: "Member",
            table: "Members",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Vehicle_Make",
            schema: "Member",
            table: "Members",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Vehicle_Model",
            schema: "Member",
            table: "Members",
            type: "nvarchar(100)",
            maxLength: 100,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Vehicle_Notes",
            schema: "Member",
            table: "Members",
            type: "nvarchar(1000)",
            maxLength: 1000,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Vehicle_Registration",
            schema: "Member",
            table: "Members",
            type: "nvarchar(20)",
            maxLength: 20,
            nullable: true);

        migrationBuilder.CreateTable(
            name: "NextOfKins",
            schema: "Member",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Relationship = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClusterId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name_Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Name_FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Name_MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Name_Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Contact_Email = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                Contact_HomeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                Contact_WorkNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                Contact_MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                Address_Address1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Address_Address2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Address_City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Address_Town = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                Address_Country = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NextOfKins", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
                table.ForeignKey(
                    name: "FK_NextOfKins_Members_MemberId",
                    column: x => x.MemberId,
                    principalSchema: "Member",
                    principalTable: "Members",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_NextOfKins_ClusterId",
            schema: "Member",
            table: "NextOfKins",
            column: "ClusterId",
            unique: true)
            .Annotation("SqlServer:Clustered", true);

        migrationBuilder.CreateIndex(
            name: "IX_NextOfKins_MemberId",
            schema: "Member",
            table: "NextOfKins",
            column: "MemberId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "NextOfKins",
            schema: "Member");

        migrationBuilder.DropColumn(
            name: "Address_Address1",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Address_Address2",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Address_City",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Address_Country",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Address_Town",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "DateOfBirth_Day",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "DateOfBirth_Month",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "DateOfBirth_Year",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "ImageUrl",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "NationalInsuranceNumber",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Vehicle_Colour",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Vehicle_Make",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Vehicle_Model",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Vehicle_Notes",
            schema: "Member",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "Vehicle_Registration",
            schema: "Member",
            table: "Members");

        migrationBuilder.AddColumn<DateTime>(
            name: "DateOfBirth_Value",
            schema: "Member",
            table: "Members",
            type: "datetime2",
            nullable: true);
    }
}
