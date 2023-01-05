using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class addRepliesInEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepliesId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_RepliesId",
                table: "Events",
                column: "RepliesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Replies_RepliesId",
                table: "Events",
                column: "RepliesId",
                principalTable: "Replies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Replies_RepliesId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_RepliesId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RepliesId",
                table: "Events");
        }
    }
}
