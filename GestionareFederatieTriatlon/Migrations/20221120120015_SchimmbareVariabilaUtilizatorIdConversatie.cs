using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaUtilizatorIdConversatie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversatii_AspNetUsers_UserId",
                table: "Conversatii");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Conversatii",
                newName: "codUtilizator");

            migrationBuilder.RenameIndex(
                name: "IX_Conversatii_UserId",
                table: "Conversatii",
                newName: "IX_Conversatii_codUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversatii_AspNetUsers_codUtilizator",
                table: "Conversatii",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversatii_AspNetUsers_codUtilizator",
                table: "Conversatii");

            migrationBuilder.RenameColumn(
                name: "codUtilizator",
                table: "Conversatii",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversatii_codUtilizator",
                table: "Conversatii",
                newName: "IX_Conversatii_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversatii_AspNetUsers_UserId",
                table: "Conversatii",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
