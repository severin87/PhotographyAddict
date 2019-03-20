using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddImageCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Images",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
