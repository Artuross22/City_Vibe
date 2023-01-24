using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class Initia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaveClubId",
                table: "Club",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Club_SaveClubId",
                table: "Club",
                column: "SaveClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_SaveClubs_SaveClubId",
                table: "Club",
                column: "SaveClubId",
                principalTable: "SaveClubs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_SaveClubs_SaveClubId",
                table: "Club");

            migrationBuilder.DropIndex(
                name: "IX_Club_SaveClubId",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "SaveClubId",
                table: "Club");
        }
    }
}
