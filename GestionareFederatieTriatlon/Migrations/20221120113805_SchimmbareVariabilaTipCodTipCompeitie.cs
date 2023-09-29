using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaTipCodTipCompeitie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitii_Tipuri_TipcodTip",
                table: "Competitii");

            migrationBuilder.RenameColumn(
                name: "TipcodTip",
                table: "Competitii",
                newName: "codTip");

            migrationBuilder.RenameIndex(
                name: "IX_Competitii_TipcodTip",
                table: "Competitii",
                newName: "IX_Competitii_codTip");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitii_Tipuri_codTip",
                table: "Competitii",
                column: "codTip",
                principalTable: "Tipuri",
                principalColumn: "codTip",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competitii_Tipuri_codTip",
                table: "Competitii");

            migrationBuilder.RenameColumn(
                name: "codTip",
                table: "Competitii",
                newName: "TipcodTip");

            migrationBuilder.RenameIndex(
                name: "IX_Competitii_codTip",
                table: "Competitii",
                newName: "IX_Competitii_TipcodTip");

            migrationBuilder.AddForeignKey(
                name: "FK_Competitii_Tipuri_TipcodTip",
                table: "Competitii",
                column: "TipcodTip",
                principalTable: "Tipuri",
                principalColumn: "codTip",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
