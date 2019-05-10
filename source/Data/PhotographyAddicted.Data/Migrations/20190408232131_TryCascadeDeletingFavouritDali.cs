using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class TryCascadeDeletingFavouritDali : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImages_Images_ImageId",
                table: "FavouriteImages");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImages_Images_ImageId",
                table: "FavouriteImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImages_Images_ImageId",
                table: "FavouriteImages");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImages_Images_ImageId",
                table: "FavouriteImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
