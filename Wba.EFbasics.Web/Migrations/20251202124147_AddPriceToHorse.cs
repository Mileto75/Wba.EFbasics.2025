using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wba.EFbasics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToHorse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Horses",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 8000m);

            migrationBuilder.UpdateData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 5000m);

            migrationBuilder.UpdateData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 23000m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Horses");
        }
    }
}
