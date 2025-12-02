using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bakery2048.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PowerUps",
                columns: new[] { "Id", "Cooldown", "Cost", "DateCreated", "DateModified", "Description", "Duration", "EffectMultiplier", "IconUrl", "IsActive", "IsUnlocked", "PowerUpName", "PowerUpType", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 3, 100, new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6420), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6420), "Doubles your score for 30 seconds", 1, 1.0, "⚡", true, true, "Score Boost", 0, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 3, 150, new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6430), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6440), "Adds 60 seconds to the timer", 1, 1.0, "⏰", true, true, "Time Extension", 1, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 3, 50, new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6440), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6450), "Undo your last move", 1, 1.0, "↩️", true, true, "Undo Move", 2, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 3, 200, new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6450), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6460), "Swap any two tiles on the board", 1, 1.0, "🔄", true, true, "Tile Swap", 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "Tiles",
                columns: new[] { "Id", "Color", "DateCreated", "DateModified", "Icon", "IsActive", "IsSpecialItem", "ItemName", "TileValue" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "#fcefe6", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6180), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6210), "🌾", true, false, "Flour", 2 },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "#f2e8cb", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6240), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6240), "🥚", true, false, "Egg", 4 },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "#f5b682", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6250), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6250), "🧈", true, false, "Butter", 8 },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "#f29446", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6260), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6260), "🍬", true, false, "Sugar", 16 },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "#f88973", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6270), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6270), "🍩", true, false, "Donut", 32 },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "#ed7056", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6280), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6280), "🍪", true, false, "Cookie", 64 },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "#ede291", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6290), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6290), "🧁", true, false, "Cupcake", 128 },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "#fce130", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6300), "🍰", true, false, "Slice Cake", 256 },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "#ffdb4a", new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6300), new DateTime(2025, 11, 30, 14, 8, 38, 612, DateTimeKind.Utc).AddTicks(6310), "🎂", true, false, "Whole Cake", 512 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "PowerUps",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"));
        }
    }
}
