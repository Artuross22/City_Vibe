using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainModelReplyAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Statement",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ReplyAppointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyAppointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyAppointment_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyAppointment_AppointmentId",
                table: "ReplyAppointment",
                column: "AppointmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyAppointment");

            migrationBuilder.DropColumn(
                name: "Statement",
                table: "Appointments");
        }
    }
}
