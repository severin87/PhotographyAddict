using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddFavourite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavouriteImageId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteImageId1",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavouriteImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteImage_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_FavouriteImageId1",
                table: "Images",
                column: "FavouriteImageId1");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteImage_PhotographyAddictedUserId",
                table: "FavouriteImage",
                column: "PhotographyAddictedUserId",
                unique: true,
                filter: "[PhotographyAddictedUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId1",
                table: "Images",
                column: "FavouriteImageId1",
                principalTable: "FavouriteImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId1",
                table: "Images");

            migrationBuilder.DropTable(
                name: "FavouriteImage");

            migrationBuilder.DropIndex(
                name: "IX_Images_FavouriteImageId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FavouriteImageId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FavouriteImageId1",
                table: "Images");
        }
    }
}
