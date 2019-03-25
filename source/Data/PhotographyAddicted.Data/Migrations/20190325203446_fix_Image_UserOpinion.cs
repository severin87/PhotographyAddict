using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class fix_Image_UserOpinion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "ImageComments",
                newName: "UserOpinion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserOpinion",
                table: "ImageComments",
                newName: "Comment");
        }
    }
}
