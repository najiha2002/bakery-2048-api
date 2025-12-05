using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bakery2048.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovePowerUps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerUps");

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(240), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(270) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(300), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(310) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(310), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(310) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(310), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(310) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(320), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(320) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(320), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(320) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(330), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(330), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(330) });

            migrationBuilder.UpdateData(
                table: "Tiles",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(330), new DateTime(2025, 12, 5, 21, 17, 36, 409, DateTimeKind.Utc).AddTicks(340) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerUps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cooldown = table.Column<int>(type: "integer", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    EffectMultiplier = table.Column<double>(type: "double precision", nullable: false),
                    IconUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsUnlocked = table.Column<bool>(type: "boolean", nullable: false),
                    PowerUpName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PowerUpType = table.Column<int>(type: "integer", nullable: false),
                    UsageCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerUps", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PowerUps",
                columns: new[] { "Id", "Cooldown", "Cost", "DateCreated", "DateModified", "Description", "Duration", "EffectMultiplier", "IconUrl", "IsActive", "IsUnlocked", "PowerUpName", "PowerUpType", "UsageCount" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), 3, 100, new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(480), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(480), "Doubles your score for 30 seconds", 1, 1.0, "⚡", true, true, "Score Boost", 0, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), 3, 150, new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(490), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(490), "Adds 60 seconds to the timer", 1, 1.0, "⏰", true, true, "Time Extension", 1, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), 3, 50, new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500), "Undo your last move", 1, 1.0, "↩️", true, true, "Undo Move", 2, 0 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), 3, 200, new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500), new DateTime(2025, 12, 2, 22, 6, 46, 742, DateTimeKind.Utc).AddTicks(500), "Swap any two tiles on the board", 1, 1.0, "🔄", true, true, "Tile Swap", 3, 0 }
                });

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
        }
    }
}
