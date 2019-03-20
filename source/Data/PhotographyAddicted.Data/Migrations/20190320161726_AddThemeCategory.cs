using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddThemeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Themes");

            migrationBuilder.AddColumn<int>(
                name: "ThemeCategory",
                table: "Themes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThemeCategory",
                table: "Themes");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Themes",
                nullable: true);
        }
    }
}
