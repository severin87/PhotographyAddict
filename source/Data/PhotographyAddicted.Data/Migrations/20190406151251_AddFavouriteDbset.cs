using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddFavouriteDbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourite_AspNetUsers_PhotographyAddictedUserId",
                table: "Favourite");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImage_Favourite_FavouriteId",
                table: "FavouriteImage");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImage_Images_ImageId",
                table: "FavouriteImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteImage",
                table: "FavouriteImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite");

            migrationBuilder.RenameTable(
                name: "FavouriteImage",
                newName: "FavouriteImages");

            migrationBuilder.RenameTable(
                name: "Favourite",
                newName: "Favourites");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteImage_ImageId",
                table: "FavouriteImages",
                newName: "IX_FavouriteImages_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Favourite_PhotographyAddictedUserId",
                table: "Favourites",
                newName: "IX_Favourites_PhotographyAddictedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteImages",
                table: "FavouriteImages",
                columns: new[] { "FavouriteId", "ImageId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImages_Favourites_FavouriteId",
                table: "FavouriteImages",
                column: "FavouriteId",
                principalTable: "Favourites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImages_Images_ImageId",
                table: "FavouriteImages",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_PhotographyAddictedUserId",
                table: "Favourites",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImages_Favourites_FavouriteId",
                table: "FavouriteImages");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImages_Images_ImageId",
                table: "FavouriteImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_PhotographyAddictedUserId",
                table: "Favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteImages",
                table: "FavouriteImages");

            migrationBuilder.RenameTable(
                name: "Favourites",
                newName: "Favourite");

            migrationBuilder.RenameTable(
                name: "FavouriteImages",
                newName: "FavouriteImage");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_PhotographyAddictedUserId",
                table: "Favourite",
                newName: "IX_Favourite_PhotographyAddictedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteImages_ImageId",
                table: "FavouriteImage",
                newName: "IX_FavouriteImage_ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourite",
                table: "Favourite",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteImage",
                table: "FavouriteImage",
                columns: new[] { "FavouriteId", "ImageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Favourite_AspNetUsers_PhotographyAddictedUserId",
                table: "Favourite",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImage_Favourite_FavouriteId",
                table: "FavouriteImage",
                column: "FavouriteId",
                principalTable: "Favourite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImage_Images_ImageId",
                table: "FavouriteImage",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
