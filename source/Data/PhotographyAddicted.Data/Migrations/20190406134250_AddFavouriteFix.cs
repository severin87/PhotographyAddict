using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddFavouriteFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_FavouriteImageId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "FavouriteImageId1",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "FavouriteImageId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_FavouriteImageId",
                table: "Images",
                column: "FavouriteImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId",
                table: "Images",
                column: "FavouriteImageId",
                principalTable: "FavouriteImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_FavouriteImageId",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "FavouriteImageId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteImageId1",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_FavouriteImageId1",
                table: "Images",
                column: "FavouriteImageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId1",
                table: "Images",
                column: "FavouriteImageId1",
                principalTable: "FavouriteImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
