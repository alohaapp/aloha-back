using Microsoft.EntityFrameworkCore.Migrations;

namespace Aloha.Model.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workstations_WorkerId",
                table: "Workstations",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkerId",
                table: "Users",
                column: "WorkerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workers_WorkerId",
                table: "Users",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workstations_Workers_WorkerId",
                table: "Workstations",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workers_WorkerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Workstations_Workers_WorkerId",
                table: "Workstations");

            migrationBuilder.DropIndex(
                name: "IX_Workstations_WorkerId",
                table: "Workstations");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);
        }
    }
}
