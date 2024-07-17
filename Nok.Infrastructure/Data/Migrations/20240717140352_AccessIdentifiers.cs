using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccessIdentifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "NextOfKins",
                schema: "Member",
                newName: "NextOfKins");

            migrationBuilder.RenameTable(
                name: "Members",
                schema: "Member",
                newName: "Members");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessIdentifierId",
                table: "Members",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccessIdentifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AzureOid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClusterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessIdentifiers", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_AccessIdentifierId",
                table: "Members",
                column: "AccessIdentifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessIdentifiers_ClusterId",
                table: "AccessIdentifiers",
                column: "ClusterId",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Members_AccessIdentifiers_AccessIdentifierId",
                table: "Members",
                column: "AccessIdentifierId",
                principalTable: "AccessIdentifiers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_AccessIdentifiers_AccessIdentifierId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "AccessIdentifiers");

            migrationBuilder.DropIndex(
                name: "IX_Members_AccessIdentifierId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "AccessIdentifierId",
                table: "Members");

            migrationBuilder.EnsureSchema(
                name: "Member");

            migrationBuilder.RenameTable(
                name: "NextOfKins",
                newName: "NextOfKins",
                newSchema: "Member");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Members",
                newSchema: "Member");
        }
    }
}
