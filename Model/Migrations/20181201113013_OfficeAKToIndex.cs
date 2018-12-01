using Microsoft.EntityFrameworkCore.Migrations;

namespace Aloha.Migrations
{
    public partial class OfficeAKToIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Offices_Name",
                table: "Offices");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_Name",
                table: "Offices",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offices_Name",
                table: "Offices");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Offices_Name",
                table: "Offices",
                column: "Name");
        }
    }
}
