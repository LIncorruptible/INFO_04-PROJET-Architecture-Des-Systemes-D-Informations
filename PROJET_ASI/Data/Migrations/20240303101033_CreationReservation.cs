using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET_ASI.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreationReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateDebut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservation_Logement_LogementID",
                        column: x => x.LogementID,
                        principalTable: "Logement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_LogementID",
                table: "Reservation",
                column: "LogementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
