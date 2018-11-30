using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aloha.Migrations
{
    public partial class FilesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Floors");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Floors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MediaType = table.Column<string>(nullable: false),
                    Data = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PhotoId",
                table: "Workers",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_ImageId",
                table: "Floors",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_Files_ImageId",
                table: "Floors",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Files_PhotoId",
                table: "Workers",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Floors_Files_ImageId",
                table: "Floors");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Files_PhotoId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Workers_PhotoId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Floors_ImageId",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Floors");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Workers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Floors",
                nullable: true);
        }
    }
}
