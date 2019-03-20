using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class FixImageCategoryProblems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ImageCategory",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCategory",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Images",
                nullable: false,
                defaultValue: 0);
        }
    }
}
