using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddVotedUsersDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VotedUsers_Images_ImageId",
                table: "VotedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_VotedUsers_Images_ImageId",
                table: "VotedUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VotedUsers_Images_ImageId",
                table: "VotedUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_VotedUsers_Images_ImageId",
                table: "VotedUsers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
