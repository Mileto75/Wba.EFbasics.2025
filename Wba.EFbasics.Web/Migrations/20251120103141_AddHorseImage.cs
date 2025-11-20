using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wba.EFbasics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddHorseImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFilename",
                table: "Horses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageFilename",
                value: null);

            migrationBuilder.UpdateData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageFilename",
                value: null);

            migrationBuilder.UpdateData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageFilename",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFilename",
                table: "Horses");
        }
    }
}
