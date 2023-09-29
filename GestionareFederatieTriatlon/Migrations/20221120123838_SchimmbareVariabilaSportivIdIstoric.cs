using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaSportivIdIstoric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Istorice_AspNetUsers_SportivId",
                table: "Istorice");

            migrationBuilder.RenameColumn(
                name: "SportivId",
                table: "Istorice",
                newName: "codUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Istorice_AspNetUsers_codUtilizator",
                table: "Istorice",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Istorice_AspNetUsers_codUtilizator",
                table: "Istorice");

            migrationBuilder.RenameColumn(
                name: "codUtilizator",
                table: "Istorice",
                newName: "SportivId");

            migrationBuilder.AddForeignKey(
                name: "FK_Istorice_AspNetUsers_SportivId",
                table: "Istorice",
                column: "SportivId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
