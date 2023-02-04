using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserInReplyUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ReplyAppointment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplyAppointment_AppUserId",
                table: "ReplyAppointment",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyAppointment_AspNetUsers_AppUserId",
                table: "ReplyAppointment",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyAppointment_AspNetUsers_AppUserId",
                table: "ReplyAppointment");

            migrationBuilder.DropIndex(
                name: "IX_ReplyAppointment_AppUserId",
                table: "ReplyAppointment");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ReplyAppointment");
        }
    }
}
