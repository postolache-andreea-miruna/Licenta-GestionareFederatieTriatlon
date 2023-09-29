using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class ModificareIstoric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "timp",
                table: "Istorice",
                newName: "timpTranzit2");

            migrationBuilder.RenameColumn(
                name: "loc",
                table: "Istorice",
                newName: "locPesteToti");

            migrationBuilder.AddColumn<int>(
                name: "locPerCatetogrie",
                table: "Istorice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "locPerGen",
                table: "Istorice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "timpAlergare",
                table: "Istorice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "timpCiclism",
                table: "Istorice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "timpInot",
                table: "Istorice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "timpTotal",
                table: "Istorice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "timpTranzit1",
                table: "Istorice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "locPerCatetogrie",
                table: "Istorice");

            migrationBuilder.DropColumn(
                name: "locPerGen",
                table: "Istorice");

            migrationBuilder.DropColumn(
                name: "timpAlergare",
                table: "Istorice");

            migrationBuilder.DropColumn(
                name: "timpCiclism",
                table: "Istorice");

            migrationBuilder.DropColumn(
                name: "timpInot",
                table: "Istorice");

            migrationBuilder.DropColumn(
                name: "timpTotal",
                table: "Istorice");

            migrationBuilder.DropColumn(
                name: "timpTranzit1",
                table: "Istorice");

            migrationBuilder.RenameColumn(
                name: "timpTranzit2",
                table: "Istorice",
                newName: "timp");

            migrationBuilder.RenameColumn(
                name: "locPesteToti",
                table: "Istorice",
                newName: "loc");
        }
    }
}
