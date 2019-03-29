using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class Adding_PhotoStory_PhotoStoryComment_and_PhotoStoryFragments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhotoStoryFragment",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Picture = table.Column<byte[]>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PhotoStoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoStoryFragment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoStoryFragment_PhotoStory_PhotoStoryId",
                        column: x => x.PhotoStoryId,
                        principalTable: "PhotoStory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoStoryFragment_PhotoStoryId",
                table: "PhotoStoryFragment",
                column: "PhotoStoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoStoryFragment");
        }
    }
}
