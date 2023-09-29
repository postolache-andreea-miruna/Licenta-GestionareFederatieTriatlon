using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaCompetitiecodCompetitieVideoclip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videoclipuri_Competitii_CompetitiecodCompetitie",
                table: "Videoclipuri");

            migrationBuilder.RenameColumn(
                name: "CompetitiecodCompetitie",
                table: "Videoclipuri",
                newName: "codCompetitie");

            migrationBuilder.RenameIndex(
                name: "IX_Videoclipuri_CompetitiecodCompetitie",
                table: "Videoclipuri",
                newName: "IX_Videoclipuri_codCompetitie");

            migrationBuilder.AddForeignKey(
                name: "FK_Videoclipuri_Competitii_codCompetitie",
                table: "Videoclipuri",
                column: "codCompetitie",
                principalTable: "Competitii",
                principalColumn: "codCompetitie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videoclipuri_Competitii_codCompetitie",
                table: "Videoclipuri");

            migrationBuilder.RenameColumn(
                name: "codCompetitie",
                table: "Videoclipuri",
                newName: "CompetitiecodCompetitie");

            migrationBuilder.RenameIndex(
                name: "IX_Videoclipuri_codCompetitie",
                table: "Videoclipuri",
                newName: "IX_Videoclipuri_CompetitiecodCompetitie");

            migrationBuilder.AddForeignKey(
                name: "FK_Videoclipuri_Competitii_CompetitiecodCompetitie",
                table: "Videoclipuri",
                column: "CompetitiecodCompetitie",
                principalTable: "Competitii",
                principalColumn: "codCompetitie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
