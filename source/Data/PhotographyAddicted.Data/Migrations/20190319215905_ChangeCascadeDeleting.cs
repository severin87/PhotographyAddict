using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class ChangeCascadeDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_PhotographyAddictedUserId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_PhotographyAddictedUserId",
                table: "Images",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_PhotographyAddictedUserId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_PhotographyAddictedUserId",
                table: "Images",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
