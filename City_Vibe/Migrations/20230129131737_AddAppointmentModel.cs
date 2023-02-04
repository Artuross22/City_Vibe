using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserAppointment",
                columns: table => new
                {
                    AppUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAppointment", x => new { x.AppUsersId, x.AppointmentsId });
                    table.ForeignKey(
                        name: "FK_AppUserAppointment_Appointment_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserAppointment_AspNetUsers_AppUsersId",
                        column: x => x.AppUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_EventId",
                table: "Appointment",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAppointment_AppointmentsId",
                table: "AppUserAppointment",
                column: "AppointmentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserAppointment");

            migrationBuilder.DropTable(
                name: "Appointment");
        }
    }
}
