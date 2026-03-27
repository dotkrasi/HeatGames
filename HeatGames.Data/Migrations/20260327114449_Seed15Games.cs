using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeatGames.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seed15Games : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games");

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Name", "Website" },
                values: new object[,]
                {
                    { new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Rockstar Games", "rockstargames.com" },
                    { new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "FromSoftware", "fromsoftware.jp" },
                    { new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Valve", "valvesoftware.com" },
                    { new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"), "CD Projekt RED", "cdprojektred.com" },
                    { new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), "Electronic Arts", "ea.com" },
                    { new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), "Ubisoft", "ubisoft.com" },
                    { new Guid("ffff0000-ffff-ffff-ffff-ffffffffffff"), "Larian Studios", "larian.com" }
                });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Name",
                value: "Екшън (Action)");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Name",
                value: "RPG (Ролеви)");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Name",
                value: "Шутър (Shooter)");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Name",
                value: "Стратегия (Strategy)");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Приключенски (Adventure)" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Независими (Indie)" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CoverImageUrl", "Description", "DeveloperId", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1174180/capsule_616x353.jpg", "Епична история за живота в безмилостната сърцевина на Америка.", new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.99m, new DateTime(2019, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/271590/capsule_616x353.jpg", "Когато млад уличен измамник и психопат се забъркват с подземния свят...", new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 59.99m, new DateTime(2015, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1091500/capsule_616x353.jpg", "Екшън-приключенска ролева игра с отворен свят, развиваща се в Найт Сити.", new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"), 119.00m, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberpunk 2077" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/292030/capsule_616x353.jpg", "Вие сте Гералт от Ривия, наемен убиец на чудовища.", new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"), 59.99m, new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/730/capsule_616x353.jpg", "Следващата ера на Counter-Strike е тук.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0.00m, new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Counter-Strike 2" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/570/capsule_616x353.jpg", "Всеки ден милиони играчи по света влизат в битка като един от над сто героя.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0.00m, new DateTime(2013, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dota 2" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/620/capsule_616x353.jpg", "Иновативен геймплей, история и музика с портали.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 19.50m, new DateTime(2011, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portal 2" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1172470/capsule_616x353.jpg", "Покорете с характер в този безплатен Hero Shooter.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 0.00m, new DateTime(2020, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apex Legends" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1774580/capsule_616x353.jpg", "Историята на Кал Кестис продължава.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 139.99m, new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars Jedi: Survivor" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1426210/capsule_616x353.jpg", "Впуснете се в най-лудото пътешествие в живота си.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 79.99m, new DateTime(2021, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "It Takes Two" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1086940/capsule_616x353.jpg", "Съберете отряда си и се завърнете във Forgotten Realms.", new Guid("ffff0000-ffff-ffff-ffff-ffffffffffff"), 119.99m, new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baldur's Gate 3" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1245620/capsule_616x353.jpg", "НОВАТА ФЕНТЪЗИ ЕКШЪН RPG ИГРА. Издигнете се и станете Elden Lord.", new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.00m, new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring" },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/814380/capsule_616x353.jpg", "Влезте в ролята на 'едноръкия вълк', опозорен и обезобразен воин.", new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.00m, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sekiro: Shadows Die Twice" },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/2208920/capsule_616x353.jpg", "Станете Ейвор, легендарен викингски рейдер.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 119.99m, new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assassin's Creed Valhalla" },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/359550/capsule_616x353.jpg", "Включете се в напрегнати, близки битки, тактика и отборна игра.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 39.99m, new DateTime(2015, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rainbow Six Siege" }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameId", "GenreId", "Id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games");

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.DeleteData(
                table: "GameGenres",
                keyColumns: new[] { "GameId", "GenreId" },
                keyValues: new object[] { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000001-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000002-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000003-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000004-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000005-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000006-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000007-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000008-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000009-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0000000a-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0000000b-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0000000c-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0000000d-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0000000e-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0000000f-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ffff0000-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Name",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Name",
                value: "RPG");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Name",
                value: "Strategy");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Name",
                value: "Shooter");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
