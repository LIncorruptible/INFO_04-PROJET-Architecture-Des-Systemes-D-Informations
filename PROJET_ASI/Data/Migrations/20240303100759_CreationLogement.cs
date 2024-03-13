using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROJET_ASI.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreationLogement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logement",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prix = table.Column<float>(type: "real", nullable: false),
                    Superficie = table.Column<float>(type: "real", nullable: false),
                    Nb_chambres = table.Column<int>(type: "int", nullable: false),
                    ProprietaireID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Logement_Proprietaire_ProprietaireID",
                        column: x => x.ProprietaireID,
                        principalTable: "Proprietaire",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logement_ProprietaireID",
                table: "Logement",
                column: "ProprietaireID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logement");
        }
    }
}
