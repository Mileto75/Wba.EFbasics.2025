using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Wba.EFbasics.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestHorse_Contest_ContestsId",
                table: "ContestHorse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contest",
                table: "Contest");

            migrationBuilder.RenameTable(
                name: "Contest",
                newName: "Contests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contests",
                table: "Contests",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Contests",
                columns: new[] { "Id", "Distance", "Location", "Name" },
                values: new object[,]
                {
                    { 1, 12.2m, "kortestraat 56, Poperinge", "Poperingse Regatta" },
                    { 2, 8.2m, "Langestraat 12, Veurne", "Veurnse Pannenkoekenrace" }
                });

            migrationBuilder.InsertData(
                table: "Identifications",
                columns: new[] { "Id", "IdentificationCode" },
                values: new object[,]
                {
                    { 1, "Alfa56" },
                    { 2, "Tango95" },
                    { 3, "Papa44" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Arabian FullBlood" },
                    { 2, "Brabants FarmerHorse" },
                    { 3, "Schoorse Shetlander Pony" }
                });

            migrationBuilder.InsertData(
                table: "Horses",
                columns: new[] { "Id", "Country", "DateOfBirth", "IdentificationId", "Name", "RaceId", "Weight" },
                values: new object[,]
                {
                    { 1, "Belgium", new DateTime(1975, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Mighty Mouse", 1, 250.3m },
                    { 2, "Italy", new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Superbad", 2, 200.3m },
                    { 3, "Germany", new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "StrudelWasser", 3, 260.3m }
                });

            migrationBuilder.InsertData(
                table: "ContestHorse",
                columns: new[] { "ContestsId", "HorsesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContestHorse_Contests_ContestsId",
                table: "ContestHorse",
                column: "ContestsId",
                principalTable: "Contests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContestHorse_Contests_ContestsId",
                table: "ContestHorse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contests",
                table: "Contests");

            migrationBuilder.DeleteData(
                table: "ContestHorse",
                keyColumns: new[] { "ContestsId", "HorsesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ContestHorse",
                keyColumns: new[] { "ContestsId", "HorsesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ContestHorse",
                keyColumns: new[] { "ContestsId", "HorsesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ContestHorse",
                keyColumns: new[] { "ContestsId", "HorsesId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ContestHorse",
                keyColumns: new[] { "ContestsId", "HorsesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ContestHorse",
                keyColumns: new[] { "ContestsId", "HorsesId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Horses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Identifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Identifications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Identifications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Contests",
                newName: "Contest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contest",
                table: "Contest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContestHorse_Contest_ContestsId",
                table: "ContestHorse",
                column: "ContestsId",
                principalTable: "Contest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
