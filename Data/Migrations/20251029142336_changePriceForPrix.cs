using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoituresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changePriceForPrix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Annonces",
                newName: "Prix");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prix",
                table: "Annonces",
                newName: "Price");
        }
    }
}
