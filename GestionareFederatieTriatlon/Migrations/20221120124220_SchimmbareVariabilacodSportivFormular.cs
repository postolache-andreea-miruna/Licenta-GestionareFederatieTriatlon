using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilacodSportivFormular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fomulare_AspNetUsers_codSportiv",
                table: "Fomulare");

            migrationBuilder.RenameColumn(
                name: "codSportiv",
                table: "Fomulare",
                newName: "codUtilizator");

            migrationBuilder.RenameIndex(
                name: "IX_Fomulare_codSportiv",
                table: "Fomulare",
                newName: "IX_Fomulare_codUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Fomulare_AspNetUsers_codUtilizator",
                table: "Fomulare",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fomulare_AspNetUsers_codUtilizator",
                table: "Fomulare");

            migrationBuilder.RenameColumn(
                name: "codUtilizator",
                table: "Fomulare",
                newName: "codSportiv");

            migrationBuilder.RenameIndex(
                name: "IX_Fomulare_codUtilizator",
                table: "Fomulare",
                newName: "IX_Fomulare_codSportiv");

            migrationBuilder.AddForeignKey(
                name: "FK_Fomulare_AspNetUsers_codSportiv",
                table: "Fomulare",
                column: "codSportiv",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
