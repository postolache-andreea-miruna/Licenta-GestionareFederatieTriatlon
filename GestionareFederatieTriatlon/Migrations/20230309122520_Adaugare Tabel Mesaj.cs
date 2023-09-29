using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareTabelMesaj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversatii");

            migrationBuilder.DropTable(
                name: "Chaturi");

            migrationBuilder.CreateTable(
                name: "Mesaje",
                columns: table => new
                {
                    codMesaj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codUtilizator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    codUtilizator2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mesajConversatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataTrimitereMesaj = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesaje", x => x.codMesaj);
                    table.ForeignKey(
                        name: "FK_Mesaje_AspNetUsers_codUtilizator",
                        column: x => x.codUtilizator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mesaje_codUtilizator",
                table: "Mesaje",
                column: "codUtilizator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mesaje");

            migrationBuilder.CreateTable(
                name: "Chaturi",
                columns: table => new
                {
                    codChat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataInfiintarii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chaturi", x => x.codChat);
                });

            migrationBuilder.CreateTable(
                name: "Conversatii",
                columns: table => new
                {
                    codConversatie = table.Column<int>(type: "int", nullable: false),
                    codUtilizator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    codChat = table.Column<int>(type: "int", nullable: false),
                    codUtilizator2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataTrimitereMesaj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    mesajConversatie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversatii", x => new { x.codConversatie, x.codUtilizator, x.codChat });
                    table.ForeignKey(
                        name: "FK_Conversatii_AspNetUsers_codUtilizator",
                        column: x => x.codUtilizator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversatii_Chaturi_codChat",
                        column: x => x.codChat,
                        principalTable: "Chaturi",
                        principalColumn: "codChat",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversatii_codChat",
                table: "Conversatii",
                column: "codChat");

            migrationBuilder.CreateIndex(
                name: "IX_Conversatii_codUtilizator",
                table: "Conversatii",
                column: "codUtilizator");
        }
    }
}
