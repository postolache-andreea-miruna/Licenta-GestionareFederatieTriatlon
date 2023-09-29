using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareAntrenorIdLaUtilizator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codAntrenor",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_codAntrenor",
                table: "AspNetUsers",
                column: "codAntrenor");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_codAntrenor",
                table: "AspNetUsers",
                column: "codAntrenor",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_codAntrenor",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_codAntrenor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "codAntrenor",
                table: "AspNetUsers");
        }
    }
}
