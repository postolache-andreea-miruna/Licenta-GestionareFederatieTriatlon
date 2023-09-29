using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class UrlPozaClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urlPozaClub",
                table: "Cluburi",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlPozaClub",
                table: "Cluburi");
        }
    }
}
