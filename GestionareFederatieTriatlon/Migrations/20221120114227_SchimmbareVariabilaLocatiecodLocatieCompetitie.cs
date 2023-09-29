using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaLocatiecodLocatieCompetitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitii_Locatii_LocatiecodLocatie",
                table: "Competitii");

            migrationBuilder.RenameColumn(
                name: "LocatiecodLocatie",
                table: "Competitii",
                newName: "codLocatie");

            migrationBuilder.RenameIndex(
                name: "IX_Competitii_LocatiecodLocatie",
                table: "Competitii",
                newName: "IX_Competitii_codLocatie");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitii_Locatii_codLocatie",
                table: "Competitii",
                column: "codLocatie",
                principalTable: "Locatii",
                principalColumn: "codLocatie",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitii_Locatii_codLocatie",
                table: "Competitii");

            migrationBuilder.RenameColumn(
                name: "codLocatie",
                table: "Competitii",
                newName: "LocatiecodLocatie");

            migrationBuilder.RenameIndex(
                name: "IX_Competitii_codLocatie",
                table: "Competitii",
                newName: "IX_Competitii_LocatiecodLocatie");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitii_Locatii_LocatiecodLocatie",
                table: "Competitii",
                column: "LocatiecodLocatie",
                principalTable: "Locatii",
                principalColumn: "codLocatie",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
