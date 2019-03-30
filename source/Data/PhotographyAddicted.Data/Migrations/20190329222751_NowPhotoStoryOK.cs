using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class NowPhotoStoryOK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStoryFragments_AspNetUsers_PhotographyAddictedUserId",
                table: "PhotoStoryFragments");

            migrationBuilder.DropIndex(
                name: "IX_PhotoStoryFragments_PhotographyAddictedUserId",
                table: "PhotoStoryFragments");

            migrationBuilder.DropColumn(
                name: "PhotographyAddictedUserId",
                table: "PhotoStoryFragments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotographyAddictedUserId",
                table: "PhotoStoryFragments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStoryFragments_PhotographyAddictedUserId",
                table: "PhotoStoryFragments",
                column: "PhotographyAddictedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStoryFragments_AspNetUsers_PhotographyAddictedUserId",
                table: "PhotoStoryFragments",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
