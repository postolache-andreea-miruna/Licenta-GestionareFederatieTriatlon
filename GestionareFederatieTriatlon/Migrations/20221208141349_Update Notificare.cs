using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotificare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "citireNotificare",
                table: "Notificari",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "codUtilizator2",
                table: "Notificari",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "dataCreare",
                table: "Notificari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "titluNotificare",
                table: "Notificari",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notificari_codUtilizator2",
                table: "Notificari",
                column: "codUtilizator2");

            migrationBuilder.AddForeignKey(
                name: "FK_Notificari_AspNetUsers_codUtilizator2",
                table: "Notificari",
                column: "codUtilizator2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notificari_AspNetUsers_codUtilizator2",
                table: "Notificari");

            migrationBuilder.DropIndex(
                name: "IX_Notificari_codUtilizator2",
                table: "Notificari");

            migrationBuilder.DropColumn(
                name: "citireNotificare",
                table: "Notificari");

            migrationBuilder.DropColumn(
                name: "codUtilizator2",
                table: "Notificari");

            migrationBuilder.DropColumn(
                name: "dataCreare",
                table: "Notificari");

            migrationBuilder.DropColumn(
                name: "titluNotificare",
                table: "Notificari");
        }
    }
}
