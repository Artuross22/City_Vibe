using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddLikeClubModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeClubId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LikeClubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Like = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeClubs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeClubs_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeClubs_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LikeClubId",
                table: "AspNetUsers",
                column: "LikeClubId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeClubs_AppUserId",
                table: "LikeClubs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeClubs_ClubId",
                table: "LikeClubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LikeClubs_LikeClubId",
                table: "AspNetUsers",
                column: "LikeClubId",
                principalTable: "LikeClubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LikeClubs_LikeClubId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LikeClubs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LikeClubId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LikeClubId",
                table: "AspNetUsers");
        }
    }
}
