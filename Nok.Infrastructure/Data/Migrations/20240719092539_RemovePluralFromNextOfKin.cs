using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nok.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePluralFromNextOfKin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NextOfKins_Members_MemberId",
                table: "NextOfKins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NextOfKins",
                table: "NextOfKins");

            migrationBuilder.RenameTable(
                name: "NextOfKins",
                newName: "NextOfKin");

            migrationBuilder.RenameIndex(
                name: "IX_NextOfKins_MemberId",
                table: "NextOfKin",
                newName: "IX_NextOfKin_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_NextOfKins_ClusterId",
                table: "NextOfKin",
                newName: "IX_NextOfKin_ClusterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NextOfKin",
                table: "NextOfKin",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_NextOfKin_Members_MemberId",
                table: "NextOfKin",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NextOfKin_Members_MemberId",
                table: "NextOfKin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NextOfKin",
                table: "NextOfKin");

            migrationBuilder.RenameTable(
                name: "NextOfKin",
                newName: "NextOfKins");

            migrationBuilder.RenameIndex(
                name: "IX_NextOfKin_MemberId",
                table: "NextOfKins",
                newName: "IX_NextOfKins_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_NextOfKin_ClusterId",
                table: "NextOfKins",
                newName: "IX_NextOfKins_ClusterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NextOfKins",
                table: "NextOfKins",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_NextOfKins_Members_MemberId",
                table: "NextOfKins",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
