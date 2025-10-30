using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoituresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnnonce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDisponibilite",
                table: "Ventes");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Annonces",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Annonces");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDisponibilite",
                table: "Ventes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Annonces",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
