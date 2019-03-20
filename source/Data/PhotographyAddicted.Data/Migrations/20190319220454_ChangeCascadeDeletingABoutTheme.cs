using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class ChangeCascadeDeletingABoutTheme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_AspNetUsers_PhotographyAddictedUserId",
                table: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_AspNetUsers_PhotographyAddictedUserId",
                table: "Themes",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Themes_AspNetUsers_PhotographyAddictedUserId",
                table: "Themes");

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_AspNetUsers_PhotographyAddictedUserId",
                table: "Themes",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
