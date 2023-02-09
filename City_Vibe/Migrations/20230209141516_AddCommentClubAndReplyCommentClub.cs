using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentClubAndReplyCommentClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForeignUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostInfoInClubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentClubs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommentClubs_PostInfoInClub_PostInfoInClubId",
                        column: x => x.PostInfoInClubId,
                        principalTable: "PostInfoInClub",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReplyCommentClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InternalUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CommentClubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyCommentClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyCommentClubs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReplyCommentClubs_CommentClubs_CommentClubId",
                        column: x => x.CommentClubId,
                        principalTable: "CommentClubs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentClubs_AppUserId",
                table: "CommentClubs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentClubs_PostInfoInClubId",
                table: "CommentClubs",
                column: "PostInfoInClubId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyCommentClubs_AppUserId",
                table: "ReplyCommentClubs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyCommentClubs_CommentClubId",
                table: "ReplyCommentClubs",
                column: "CommentClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyCommentClubs");

            migrationBuilder.DropTable(
                name: "CommentClubs");
        }
    }
}
