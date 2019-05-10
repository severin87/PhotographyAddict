using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotographyAddicted.Data.Migrations
{
    public partial class AddedConversationAndMesage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Subject = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    SenderPhotographyAddictedUserId = table.Column<string>(nullable: true),
                    RecepientPhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_RecepientPhotographyAddictedUserId",
                        column: x => x.RecepientPhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Conversations_AspNetUsers_SenderPhotographyAddictedUserId",
                        column: x => x.SenderPhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ConversationId = table.Column<string>(nullable: true),
                    ConversationId1 = table.Column<int>(nullable: true),
                    PhotographyAddictedUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId1",
                        column: x => x.ConversationId1,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_PhotographyAddictedUserId",
                        column: x => x.PhotographyAddictedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_RecepientPhotographyAddictedUserId",
                table: "Conversations",
                column: "RecepientPhotographyAddictedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_SenderPhotographyAddictedUserId",
                table: "Conversations",
                column: "SenderPhotographyAddictedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId1",
                table: "Messages",
                column: "ConversationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_PhotographyAddictedUserId",
                table: "Messages",
                column: "PhotographyAddictedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");
        }
    }
}
