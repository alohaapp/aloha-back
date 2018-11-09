using Microsoft.EntityFrameworkCore.Migrations;

namespace Aloha.Migrations
{
    public partial class RequiredsAndFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workers_WorkerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Workstations_Floors_FloorId",
                table: "Workstations");

            migrationBuilder.DropForeignKey(
                name: "FK_Workstations_Workers_WorkerId",
                table: "Workstations");

            migrationBuilder.DropIndex(
                name: "IX_Workstations_WorkerId",
                table: "Workstations");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Workstations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FloorId",
                table: "Workstations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Floors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workstations_WorkerId",
                table: "Workstations",
                column: "WorkerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workers_WorkerId",
                table: "Users",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workstations_Floors_FloorId",
                table: "Workstations",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workstations_Workers_WorkerId",
                table: "Workstations",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Workers_WorkerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Workstations_Floors_FloorId",
                table: "Workstations");

            migrationBuilder.DropForeignKey(
                name: "FK_Workstations_Workers_WorkerId",
                table: "Workstations");

            migrationBuilder.DropIndex(
                name: "IX_Workstations_WorkerId",
                table: "Workstations");

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Workstations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FloorId",
                table: "Workstations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Workers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Floors",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Workstations_WorkerId",
                table: "Workstations",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Workers_WorkerId",
                table: "Users",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workstations_Floors_FloorId",
                table: "Workstations",
                column: "FloorId",
                principalTable: "Floors",
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
    }
}
