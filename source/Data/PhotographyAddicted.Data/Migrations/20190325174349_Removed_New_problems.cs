using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class Removed_New_problems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewComments_News_NewId",
                table: "NewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments");

            migrationBuilder.AddForeignKey(
                name: "FK_NewComments_News_NewId",
                table: "NewComments",
                column: "NewId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewComments_News_NewId",
                table: "NewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments");

            migrationBuilder.AddForeignKey(
                name: "FK_NewComments_News_NewId",
                table: "NewComments",
                column: "NewId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
