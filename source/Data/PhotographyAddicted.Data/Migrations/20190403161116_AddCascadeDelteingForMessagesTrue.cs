using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddCascadeDelteingForMessagesTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_SenderPhotographyAddictedUserId",
                table: "Conversations");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_SenderPhotographyAddictedUserId",
                table: "Conversations",
                column: "SenderPhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_AspNetUsers_SenderPhotographyAddictedUserId",
                table: "Conversations");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_AspNetUsers_SenderPhotographyAddictedUserId",
                table: "Conversations",
                column: "SenderPhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
