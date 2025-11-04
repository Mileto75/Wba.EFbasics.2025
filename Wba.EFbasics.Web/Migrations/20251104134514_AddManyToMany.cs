using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wba.EFbasics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContestHorse",
                columns: table => new
                {
                    ContestsId = table.Column<int>(type: "int", nullable: false),
                    HorsesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestHorse", x => new { x.ContestsId, x.HorsesId });
                    table.ForeignKey(
                        name: "FK_ContestHorse_Contest_ContestsId",
                        column: x => x.ContestsId,
                        principalTable: "Contest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContestHorse_Horses_HorsesId",
                        column: x => x.HorsesId,
                        principalTable: "Horses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContestHorse_HorsesId",
                table: "ContestHorse",
                column: "HorsesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestHorse");

            migrationBuilder.DropTable(
                name: "Contest");
        }
    }
}
