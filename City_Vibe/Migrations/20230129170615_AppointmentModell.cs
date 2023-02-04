using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentModell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AppUserAppointment");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_AppUserId",
                table: "Appointments",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_AppUserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppUserId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                        name: "FK_AppUserAppointment_Appointments_AppointmentsId",
                        column: x => x.AppointmentsId,
                        principalTable: "Appointments",
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
                name: "IX_AppUserAppointment_AppointmentsId",
                table: "AppUserAppointment",
                column: "AppointmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Events_EventId",
                table: "Appointments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
