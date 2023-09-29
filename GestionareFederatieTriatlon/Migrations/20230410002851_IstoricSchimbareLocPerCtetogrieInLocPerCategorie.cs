using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class IstoricSchimbareLocPerCtetogrieInLocPerCategorie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "locPerCatetogrie",
                table: "Istorice",
                newName: "locPerCategorie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "locPerCategorie",
                table: "Istorice",
                newName: "locPerCatetogrie");
        }
    }
}
