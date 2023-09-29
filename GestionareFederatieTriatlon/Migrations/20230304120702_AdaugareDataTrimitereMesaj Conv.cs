using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareDataTrimitereMesajConv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dataLivrareMesaj",
                table: "Conversatii");

            migrationBuilder.RenameColumn(
                name: "dataPrimireMesaj",
                table: "Conversatii",
                newName: "dataTrimitereMesaj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dataTrimitereMesaj",
                table: "Conversatii",
                newName: "dataPrimireMesaj");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataLivrareMesaj",
                table: "Conversatii",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
