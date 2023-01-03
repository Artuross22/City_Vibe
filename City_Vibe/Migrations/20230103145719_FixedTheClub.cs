using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class FixedTheClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Club",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Club_EventId",
                table: "Club",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Club_Events_EventId",
                table: "Club",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Club_Events_EventId",
                table: "Club");

            migrationBuilder.DropIndex(
                name: "IX_Club_EventId",
                table: "Club");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Club");
        }
    }
}
