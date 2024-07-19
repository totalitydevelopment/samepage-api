using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeToAccessIdentifierTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AccessIdentifiers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "AccessIdentifiers");
        }
    }
}
