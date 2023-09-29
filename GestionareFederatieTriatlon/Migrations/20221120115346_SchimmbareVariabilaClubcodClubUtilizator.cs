using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class SchimmbareVariabilaClubcodClubUtilizator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cluburi_ClubcodClub",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ClubcodClub",
                table: "AspNetUsers",
                newName: "codClub");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_ClubcodClub",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_codClub");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cluburi_codClub",
                table: "AspNetUsers",
                column: "codClub",
                principalTable: "Cluburi",
                principalColumn: "codClub",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cluburi_codClub",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "codClub",
                table: "AspNetUsers",
                newName: "ClubcodClub");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_codClub",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_ClubcodClub");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cluburi_ClubcodClub",
                table: "AspNetUsers",
                column: "ClubcodClub",
                principalTable: "Cluburi",
                principalColumn: "codClub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
