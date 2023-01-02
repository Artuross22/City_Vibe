using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityVibe.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressInEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Address",
                newName: "Region");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_AddressId",
                table: "Events",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Address_AddressId",
                table: "Events",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Address_AddressId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_AddressId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Address",
                newName: "State");
        }
    }
}
