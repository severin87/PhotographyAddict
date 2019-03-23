using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddNew_NewCommentFInaly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    NewImage = table.Column<byte[]>(nullable: true),
                    TextContent = table.Column<string>(nullable: true),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true),
                    ComentsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                    table.ForeignKey(
                        name: "FK_News_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserOpinion = table.Column<string>(nullable: true),
                    NewId = table.Column<int>(nullable: true),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewComments_News_NewId",
                        column: x => x.NewId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NewComments_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewComments_NewId",
                table: "NewComments",
                column: "NewId");

            migrationBuilder.CreateIndex(
                name: "IX_NewComments_PhotographyAddictedUserId",
                table: "NewComments",
                column: "PhotographyAddictedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_News_PhotographyAddictedUserId",
                table: "News",
                column: "PhotographyAddictedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewComments");

            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
