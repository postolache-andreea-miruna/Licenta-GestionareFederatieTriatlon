using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaUtilizatorIdNotificare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificari_AspNetUsers_UtilizatorId",
                table: "Notificari");

            migrationBuilder.RenameColumn(
                name: "UtilizatorId",
                table: "Notificari",
                newName: "codUtilizator");

            migrationBuilder.RenameIndex(
                name: "IX_Notificari_UtilizatorId",
                table: "Notificari",
                newName: "IX_Notificari_codUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificari_AspNetUsers_codUtilizator",
                table: "Notificari",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificari_AspNetUsers_codUtilizator",
                table: "Notificari");

            migrationBuilder.RenameColumn(
                name: "codUtilizator",
                table: "Notificari",
                newName: "UtilizatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Notificari_codUtilizator",
                table: "Notificari",
                newName: "IX_Notificari_UtilizatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificari_AspNetUsers_UtilizatorId",
                table: "Notificari",
                column: "UtilizatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
