using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wba.EFbasics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Races",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "IdentificationId",
                table: "Horses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Identifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horses_IdentificationId",
                table: "Horses",
                column: "IdentificationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Horses_Identifications_IdentificationId",
                table: "Horses",
                column: "IdentificationId",
                principalTable: "Identifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horses_Identifications_IdentificationId",
                table: "Horses");

            migrationBuilder.DropTable(
                name: "Identifications");

            migrationBuilder.DropIndex(
                name: "IX_Horses_IdentificationId",
                table: "Horses");

            migrationBuilder.DropColumn(
                name: "IdentificationId",
                table: "Horses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Races",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
