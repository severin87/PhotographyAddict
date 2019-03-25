using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class Add_NewComment_HAshSet_to_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_PhotographyAddictedUserId",
                table: "News");

            migrationBuilder.AddForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_PhotographyAddictedUserId",
                table: "News",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments");

            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_PhotographyAddictedUserId",
                table: "News");

            migrationBuilder.AddForeignKey(
                name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                table: "NewComments",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_PhotographyAddictedUserId",
                table: "News",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
