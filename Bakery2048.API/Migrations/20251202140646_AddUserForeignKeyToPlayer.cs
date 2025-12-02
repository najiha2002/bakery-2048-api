using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakery2048.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserForeignKeyToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(480), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(480) });

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(490), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(490) });

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(330), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(390), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(390) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(400), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(400) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(410), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(410), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(410), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(410) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(420), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(420), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(420) });

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Users_UserId",
                table: "Players",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Users_UserId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_UserId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Players");

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6420) });

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6440) });

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6450) });

            migrationBuilder.UpdateData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6460) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6210) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6240) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6250) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6260) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6270) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6280) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6290) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6300) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6310) });
        }
    }
}
