using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AddcodConexiuneUtilizator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "codConexiune",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "codConexiune",
                table: "AspNetUsers");
        }
    }
}
