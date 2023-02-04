using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class addAppointmentModelRe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Events_EventId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserAppointment_Appointment_AppointmentsId",
                table: "AppUserAppointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment");

            migrationBuilder.RenameTable(
                name: "Appointment",
                newName: "Appointments");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_EventId",
                table: "Appointments",
                newName: "IX_Appointments_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserAppointment_Appointments_AppointmentsId",
                table: "AppUserAppointment",
                column: "AppointmentsId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserAppointment_Appointments_AppointmentsId",
                table: "AppUserAppointment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "Appointment");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_EventId",
                table: "Appointment",
                newName: "IX_Appointment_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointment",
                table: "Appointment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Events_EventId",
                table: "Appointment",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserAppointment_Appointment_AppointmentsId",
                table: "AppUserAppointment",
                column: "AppointmentsId",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
