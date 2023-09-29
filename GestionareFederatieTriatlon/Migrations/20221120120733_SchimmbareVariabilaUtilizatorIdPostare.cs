using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaUtilizatorIdPostare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postari_AspNetUsers_UtilizatorId",
                table: "Postari");

            migrationBuilder.RenameColumn(
                name: "UtilizatorId",
                table: "Postari",
                newName: "codUtilizator");

            migrationBuilder.RenameIndex(
                name: "IX_Postari_UtilizatorId",
                table: "Postari",
                newName: "IX_Postari_codUtilizator");

            migrationBuilder.AddForeignKey(
                name: "FK_Postari_AspNetUsers_codUtilizator",
                table: "Postari",
                column: "codUtilizator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postari_AspNetUsers_codUtilizator",
                table: "Postari");

            migrationBuilder.RenameColumn(
                name: "codUtilizator",
                table: "Postari",
                newName: "UtilizatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Postari_codUtilizator",
                table: "Postari",
                newName: "IX_Postari_UtilizatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postari_AspNetUsers_UtilizatorId",
                table: "Postari",
                column: "UtilizatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
