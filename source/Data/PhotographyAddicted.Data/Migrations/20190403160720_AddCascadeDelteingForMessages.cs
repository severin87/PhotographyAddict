using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddCascadeDelteingForMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_PhotographyAddictedUserId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_PhotographyAddictedUserId",
                table: "Messages",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_PhotographyAddictedUserId",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_PhotographyAddictedUserId",
                table: "Messages",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
