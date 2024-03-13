using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET_ASI.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreationComporte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comporte",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    LogementID = table.Column<int>(type: "int", nullable: false),
                    EquipementID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comporte", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comporte_Equipement_EquipementID",
                        column: x => x.EquipementID,
                        principalTable: "Equipement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comporte_Logement_LogementID",
                        column: x => x.LogementID,
                        principalTable: "Logement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comporte_EquipementID",
                table: "Comporte",
                column: "EquipementID");

            migrationBuilder.CreateIndex(
                name: "IX_Comporte_LogementID",
                table: "Comporte",
                column: "LogementID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comporte");
        }
    }
}
