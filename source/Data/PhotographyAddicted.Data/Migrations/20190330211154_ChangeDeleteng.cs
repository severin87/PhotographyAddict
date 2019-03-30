using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class ChangeDeleteng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStories_AspNetUsers_PhotographyAddictedUserId",
                table: "PhotoStories");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStoryComments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStoryFragments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryFragments");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStories_AspNetUsers_PhotographyAddictedUserId",
                table: "PhotoStories",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStoryComments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryComments",
                column: "PhotoStoryId",
                principalTable: "PhotoStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStoryFragments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryFragments",
                column: "PhotoStoryId",
                principalTable: "PhotoStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStories_AspNetUsers_PhotographyAddictedUserId",
                table: "PhotoStories");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStoryComments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoStoryFragments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryFragments");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStories_AspNetUsers_PhotographyAddictedUserId",
                table: "PhotoStories",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStoryComments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryComments",
                column: "PhotoStoryId",
                principalTable: "PhotoStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoStoryFragments_PhotoStories_PhotoStoryId",
                table: "PhotoStoryFragments",
                column: "PhotoStoryId",
                principalTable: "PhotoStories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
