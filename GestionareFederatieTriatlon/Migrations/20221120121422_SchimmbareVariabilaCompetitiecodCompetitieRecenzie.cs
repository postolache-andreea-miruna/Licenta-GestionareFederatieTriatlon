using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaCompetitiecodCompetitieRecenzie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_Competitii_CompetitiecodCompetitie",
                table: "Recenzii");

            migrationBuilder.RenameColumn(
                name: "CompetitiecodCompetitie",
                table: "Recenzii",
                newName: "codCompetitie");

            migrationBuilder.RenameIndex(
                name: "IX_Recenzii_CompetitiecodCompetitie",
                table: "Recenzii",
                newName: "IX_Recenzii_codCompetitie");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_Competitii_codCompetitie",
                table: "Recenzii",
                column: "codCompetitie",
                principalTable: "Competitii",
                principalColumn: "codCompetitie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_Competitii_codCompetitie",
                table: "Recenzii");

            migrationBuilder.RenameColumn(
                name: "codCompetitie",
                table: "Recenzii",
                newName: "CompetitiecodCompetitie");

            migrationBuilder.RenameIndex(
                name: "IX_Recenzii_codCompetitie",
                table: "Recenzii",
                newName: "IX_Recenzii_CompetitiecodCompetitie");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_Competitii_CompetitiecodCompetitie",
                table: "Recenzii",
                column: "CompetitiecodCompetitie",
                principalTable: "Competitii",
                principalColumn: "codCompetitie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
