using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddedEquipmenAndSettingsImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipment",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Settings",
                table: "Images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipment",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Settings",
                table: "Images");
        }
    }
}
