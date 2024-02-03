using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Member");

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name_Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name_FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name_Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contact_Email = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Contact_HomeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Contact_WorkNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Contact_MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DateOfBirth_Value = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_ClusterId",
                schema: "Member",
                table: "Members",
                column: "ClusterId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members",
                schema: "Member");
        }
    }
}
