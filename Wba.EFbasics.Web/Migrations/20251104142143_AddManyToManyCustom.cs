using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wba.EFbasics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddManyToManyCustom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestHorse_Contest_ContestsId",
                table: "ContestHorse");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestHorse_Horses_HorsesId",
                table: "ContestHorse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestHorse",
                table: "ContestHorse");

            migrationBuilder.DropIndex(
                name: "IX_ContestHorse_HorsesId",
                table: "ContestHorse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contest",
                table: "Contest");

            migrationBuilder.RenameTable(
                name: "Contest",
                newName: "Contests");

            migrationBuilder.RenameColumn(
                name: "HorsesId",
                table: "ContestHorse",
                newName: "Ranking");

            migrationBuilder.RenameColumn(
                name: "ContestsId",
                table: "ContestHorse",
                newName: "HorseId");

            migrationBuilder.AddColumn<int>(
                name: "ContestId",
                table: "ContestHorse",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestHorse",
                table: "ContestHorse",
                columns: new[] { "ContestId", "HorseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contests",
                table: "Contests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestHorse_HorseId",
                table: "ContestHorse",
                column: "HorseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestHorse_Contests_ContestId",
                table: "ContestHorse",
                column: "ContestId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestHorse_Horses_HorseId",
                table: "ContestHorse",
                column: "HorseId",
                principalTable: "Horses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestHorse_Contests_ContestId",
                table: "ContestHorse");

            migrationBuilder.DropForeignKey(
                name: "FK_ContestHorse_Horses_HorseId",
                table: "ContestHorse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContestHorse",
                table: "ContestHorse");

            migrationBuilder.DropIndex(
                name: "IX_ContestHorse_HorseId",
                table: "ContestHorse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contests",
                table: "Contests");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "ContestHorse");

            migrationBuilder.RenameTable(
                name: "Contests",
                newName: "Contest");

            migrationBuilder.RenameColumn(
                name: "Ranking",
                table: "ContestHorse",
                newName: "HorsesId");

            migrationBuilder.RenameColumn(
                name: "HorseId",
                table: "ContestHorse",
                newName: "ContestsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContestHorse",
                table: "ContestHorse",
                columns: new[] { "ContestsId", "HorsesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contest",
                table: "Contest",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContestHorse_HorsesId",
                table: "ContestHorse",
                column: "HorsesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestHorse_Contest_ContestsId",
                table: "ContestHorse",
                column: "ContestsId",
                principalTable: "Contest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContestHorse_Horses_HorsesId",
                table: "ContestHorse",
                column: "HorsesId",
                principalTable: "Horses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
