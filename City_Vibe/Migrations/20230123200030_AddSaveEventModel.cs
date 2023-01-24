using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddSaveEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SaveEventId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SaveEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveEvents_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaveEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SaveEventId",
                table: "Events",
                column: "SaveEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveEvents_AppUserId",
                table: "SaveEvents",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveEvents_EventId",
                table: "SaveEvents",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SaveEvents_SaveEventId",
                table: "Events",
                column: "SaveEventId",
                principalTable: "SaveEvents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_SaveEvents_SaveEventId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "SaveEvents");

            migrationBuilder.DropIndex(
                name: "IX_Events_SaveEventId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SaveEventId",
                table: "Events");
        }
    }
}
