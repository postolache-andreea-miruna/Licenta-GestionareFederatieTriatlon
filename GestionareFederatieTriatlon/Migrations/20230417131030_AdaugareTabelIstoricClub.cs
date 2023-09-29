using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareTabelIstoricClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IstoriceCluburi",
                columns: table => new
                {
                    codUtilizator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    codClub = table.Column<int>(type: "int", nullable: false),
                    dataInscriereClub = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dataParasireClub = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstoriceCluburi", x => new { x.codUtilizator, x.codClub, x.dataInscriereClub });
                    table.ForeignKey(
                        name: "FK_IstoriceCluburi_AspNetUsers_codUtilizator",
                        column: x => x.codUtilizator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IstoriceCluburi_Cluburi_codClub",
                        column: x => x.codClub,
                        principalTable: "Cluburi",
                        principalColumn: "codClub");
                });

            migrationBuilder.CreateIndex(
                name: "IX_IstoriceCluburi_codClub",
                table: "IstoriceCluburi",
                column: "codClub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IstoriceCluburi");
        }
    }
}
