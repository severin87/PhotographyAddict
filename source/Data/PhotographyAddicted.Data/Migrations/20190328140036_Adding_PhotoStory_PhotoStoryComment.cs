using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class Adding_PhotoStory_PhotoStoryComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoStory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UploadedDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Introduction = table.Column<string>(nullable: true),
                    Conclusion = table.Column<string>(nullable: true),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoStory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoStory_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoStoryComment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpinion = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    PhotoStoryId = table.Column<int>(nullable: true),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoStoryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoStoryComment_PhotoStory_PhotoStoryId",
                        column: x => x.PhotoStoryId,
                        principalTable: "PhotoStory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhotoStoryComment_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStory_PhotographyAddictedUserId",
                table: "PhotoStory",
                column: "PhotographyAddictedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStoryComment_PhotoStoryId",
                table: "PhotoStoryComment",
                column: "PhotoStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStoryComment_PhotographyAddictedUserId",
                table: "PhotoStoryComment",
                column: "PhotographyAddictedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoStoryComment");

            migrationBuilder.DropTable(
                name: "PhotoStory");
        }
    }
}
