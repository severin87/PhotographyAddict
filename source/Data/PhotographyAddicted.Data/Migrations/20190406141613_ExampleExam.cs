using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class ExampleExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImage_AspNetUsers_PhotographyAddictedUserId",
                table: "FavouriteImage");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_FavouriteImageId",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteImage",
                table: "FavouriteImage");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteImage_PhotographyAddictedUserId",
                table: "FavouriteImage");

            migrationBuilder.DropColumn(
                name: "FavouriteImageId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FavouriteImage");

            migrationBuilder.RenameColumn(
                name: "PhotographyAddictedUserId",
                table: "FavouriteImage",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FavouriteImage",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavouriteId",
                table: "FavouriteImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "FavouriteImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteImage",
                table: "FavouriteImage",
                columns: new[] { "FavouriteId", "ImageId" });

            migrationBuilder.CreateTable(
                name: "Favourite",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourite_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteImage_ImageId",
                table: "FavouriteImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourite_PhotographyAddictedUserId",
                table: "Favourite",
                column: "PhotographyAddictedUserId",
                unique: true,
                filter: "[PhotographyAddictedUserId] IS NOT NULL");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImage_Favourite_FavouriteId",
                table: "FavouriteImage");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteImage_Images_ImageId",
                table: "FavouriteImage");

            migrationBuilder.DropTable(
                name: "Favourite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteImage",
                table: "FavouriteImage");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteImage_ImageId",
                table: "FavouriteImage");

            migrationBuilder.DropColumn(
                name: "FavouriteId",
                table: "FavouriteImage");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "FavouriteImage");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FavouriteImage",
                newName: "PhotographyAddictedUserId");

            migrationBuilder.AddColumn<int>(
                name: "FavouriteImageId",
                table: "Images",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotographyAddictedUserId",
                table: "FavouriteImage",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FavouriteImage",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteImage",
                table: "FavouriteImage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Images_FavouriteImageId",
                table: "Images",
                column: "FavouriteImageId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteImage_PhotographyAddictedUserId",
                table: "FavouriteImage",
                column: "PhotographyAddictedUserId",
                unique: true,
                filter: "[PhotographyAddictedUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteImage_AspNetUsers_PhotographyAddictedUserId",
                table: "FavouriteImage",
                column: "PhotographyAddictedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_FavouriteImage_FavouriteImageId",
                table: "Images",
                column: "FavouriteImageId",
                principalTable: "FavouriteImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
