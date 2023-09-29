using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilacodSportivRecenzie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_AspNetUsers_codSportiv",
                table: "Recenzii");

            migrationBuilder.RenameColumn(
                name: "codSportiv",
                table: "Recenzii",
                newName: "codUtilizator");

            migrationBuilder.RenameIndex(
                name: "IX_Recenzii_codSportiv",
                table: "Recenzii",
                newName: "IX_Recenzii_codUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_AspNetUsers_codUtilizator",
                table: "Recenzii",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recenzii_AspNetUsers_codUtilizator",
                table: "Recenzii");

            migrationBuilder.RenameColumn(
                name: "codUtilizator",
                table: "Recenzii",
                newName: "codSportiv");

            migrationBuilder.RenameIndex(
                name: "IX_Recenzii_codUtilizator",
                table: "Recenzii",
                newName: "IX_Recenzii_codSportiv");

            migrationBuilder.AddForeignKey(
                name: "FK_Recenzii_AspNetUsers_codSportiv",
                table: "Recenzii",
                column: "codSportiv",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
