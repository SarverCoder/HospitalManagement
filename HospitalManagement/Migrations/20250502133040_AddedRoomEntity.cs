using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HospitalManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "room_id",
                table: "appointments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    room_number = table.Column<string>(type: "text", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_appointments_room_id",
                table: "appointments",
                column: "room_id");

            migrationBuilder.AddForeignKey(
                name: "fk_appointments_rooms_room_id",
                table: "appointments",
                column: "room_id",
                principalTable: "rooms",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_appointments_rooms_room_id",
                table: "appointments");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropIndex(
                name: "ix_appointments_room_id",
                table: "appointments");

            migrationBuilder.DropColumn(
                name: "room_id",
                table: "appointments");
        }
    }
}
