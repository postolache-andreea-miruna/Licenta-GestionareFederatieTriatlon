using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaSportivIdFormmular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fomulare_AspNetUsers_SportivId",
                table: "Fomulare");

            migrationBuilder.RenameColumn(
                name: "SportivId",
                table: "Fomulare",
                newName: "codSportiv");

            migrationBuilder.RenameIndex(
                name: "IX_Fomulare_SportivId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fomulare_AspNetUsers_codSportiv",
                table: "Fomulare");

            migrationBuilder.RenameColumn(
                name: "codSportiv",
                table: "Fomulare",
                newName: "SportivId");

            migrationBuilder.RenameIndex(
                name: "IX_Fomulare_codSportiv",
                table: "Fomulare",
                newName: "IX_Fomulare_SportivId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fomulare_AspNetUsers_SportivId",
                table: "Fomulare",
                column: "SportivId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
