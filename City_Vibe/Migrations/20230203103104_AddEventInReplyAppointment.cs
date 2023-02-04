using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddEventInReplyAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "ReplyAppointment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplyAppointment_EventId",
                table: "ReplyAppointment",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyAppointment_Events_EventId",
                table: "ReplyAppointment",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyAppointment_Events_EventId",
                table: "ReplyAppointment");

            migrationBuilder.DropIndex(
                name: "IX_ReplyAppointment_EventId",
                table: "ReplyAppointment");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "ReplyAppointment");
        }
    }
}
