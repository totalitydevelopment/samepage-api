using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessIdentifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AzureOid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessIdentifiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails_HomeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_WorkNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth_Year = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth_Month = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth_Day = table.Column<int>(type: "int", nullable: true),
                    Vehicle_RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vehicle_Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Colour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle_Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessIdentifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_AccessIdentifiers_AccessIdentifierId",
                        column: x => x.AccessIdentifierId,
                        principalTable: "AccessIdentifiers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Postcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth_Year = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth_Month = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth_Day = table.Column<int>(type: "int", nullable: true),
                    ContactDetails_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactDetails_HomeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_WorkNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDetails_MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NextOfKin_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_AccessIdentifierId",
                table: "Members",
                column: "AccessIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_MemberId",
                table: "NextOfKin",
                column: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NextOfKin");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "AccessIdentifiers");
        }
    }
}
