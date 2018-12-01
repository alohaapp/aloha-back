using Microsoft.EntityFrameworkCore.Migrations;

namespace Aloha.Migrations
{
    public partial class AkOnOfficeAndUserIdWorker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workers_UserId",
                table: "Workers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offices",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Workers_UserId",
                table: "Workers",
                column: "UserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Offices_Name",
                table: "Offices",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Workers_UserId",
                table: "Workers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Offices_Name",
                table: "Offices");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Offices",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Workers_UserId",
                table: "Workers",
                column: "UserId",
                unique: true);
        }
    }
}
