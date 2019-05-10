using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class ExpanedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Blocked",
                table: "AspNetUsers",
                newName: "BannedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "BanLength",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanLength",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "BannedDate",
                table: "AspNetUsers",
                newName: "Blocked");
        }
    }
}
