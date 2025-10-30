using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoituresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeDecimalForInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Prix",
                table: "Annonces",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Prix",
                table: "Annonces",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
