using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeatGames.Data.Migrations
{
    /// <inheritdoc />
    public partial class HeatGames2Mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatforms",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatforms_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), "PC (Windows)" },
                    { new Guid("22222222-3333-4444-5555-666666666666"), "PlayStation 5" },
                    { new Guid("33333333-4444-5555-6666-777777777777"), "Xbox Series X/S" },
                    { new Guid("44444444-5555-6666-7777-888888888888"), "Nintendo Switch" }
                });

            migrationBuilder.InsertData(
                table: "GamePlatforms",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformId",
                table: "GamePlatforms",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "Platforms");
        }
    }
}
