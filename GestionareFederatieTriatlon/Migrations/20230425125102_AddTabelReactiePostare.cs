using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelReactiePostare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReactiiPostari",
                columns: table => new
                {
                    codUtilizator = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    codPostare = table.Column<int>(type: "int", nullable: false),
                    reactieFericire = table.Column<bool>(type: "bit", nullable: false),
                    reactieTristete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactiiPostari", x => new { x.codUtilizator, x.codPostare });
                    table.ForeignKey(
                        name: "FK_ReactiiPostari_AspNetUsers_codUtilizator",
                        column: x => x.codUtilizator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReactiiPostari_Postari_codPostare",
                        column: x => x.codPostare,
                        principalTable: "Postari",
                        principalColumn: "codPostare");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReactiiPostari_codPostare",
                table: "ReactiiPostari",
                column: "codPostare");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReactiiPostari");
        }
    }
}
