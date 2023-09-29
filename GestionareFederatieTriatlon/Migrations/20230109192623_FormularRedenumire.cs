using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class FormularRedenumire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fomulare_AspNetUsers_codUtilizator",
                table: "Fomulare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fomulare",
                table: "Fomulare");

            migrationBuilder.RenameTable(
                name: "Fomulare",
                newName: "Formulare");

            migrationBuilder.RenameIndex(
                name: "IX_Fomulare_codUtilizator",
                table: "Formulare",
                newName: "IX_Formulare_codUtilizator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Formulare",
                table: "Formulare",
                column: "codFormular");

            migrationBuilder.AddForeignKey(
                name: "FK_Formulare_AspNetUsers_codUtilizator",
                table: "Formulare",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Formulare_AspNetUsers_codUtilizator",
                table: "Formulare");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Formulare",
                table: "Formulare");

            migrationBuilder.RenameTable(
                name: "Formulare",
                newName: "Fomulare");

            migrationBuilder.RenameIndex(
                name: "IX_Formulare_codUtilizator",
                table: "Fomulare",
                newName: "IX_Fomulare_codUtilizator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fomulare",
                table: "Fomulare",
                column: "codFormular");

            migrationBuilder.AddForeignKey(
                name: "FK_Fomulare_AspNetUsers_codUtilizator",
                table: "Fomulare",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
