using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HeatGames.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WalletBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeveloperId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameGenres",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenres", x => new { x.GameId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_GameGenres_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "LibraryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayTimeMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriceAtPurchase = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPositive = table.Column<bool>(type: "bit", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Екшън (Action)" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "RPG (Ролеви)" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Шутър (Shooter)" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Стратегия (Strategy)" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Приключенски (Adventure)" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Независими (Indie)" }
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
                table: "Games",
                columns: new[] { "Id", "CoverImageUrl", "Description", "DeveloperId", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1174180/capsule_616x353.jpg", "Епична история за живота в безмилостната сърцевина на Америка.", new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.99m, new DateTime(2019, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2" },
                    { new Guid("00000002-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/271590/capsule_616x353.jpg", "Когато млад уличен измамник и психопат се забъркват с подземния свят...", new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 59.99m, new DateTime(2015, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("00000003-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1091500/capsule_616x353.jpg", "Екшън-приключенска ролева игра с отворен свят, развиваща се в Найт Сити.", new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"), 119.00m, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberpunk 2077" },
                    { new Guid("00000004-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/292030/capsule_616x353.jpg", "Вие сте Гералт от Ривия, наемен убиец на чудовища.", new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"), 59.99m, new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt" },
                    { new Guid("00000005-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/730/capsule_616x353.jpg", "Следващата ера на Counter-Strike е тук.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0.00m, new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Counter-Strike 2" },
                    { new Guid("00000006-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/570/capsule_616x353.jpg", "Всеки ден милиони играчи по света влизат в битка.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0.00m, new DateTime(2013, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dota 2" },
                    { new Guid("00000007-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/620/capsule_616x353.jpg", "Иновативен геймплей, история и музика с портали.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 19.50m, new DateTime(2011, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portal 2" },
                    { new Guid("00000008-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1172470/capsule_616x353.jpg", "Покорете с характер в този безплатен Hero Shooter.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 0.00m, new DateTime(2020, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apex Legends" },
                    { new Guid("00000009-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1774580/capsule_616x353.jpg", "Историята на Кал Кестис продължава.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 139.99m, new DateTime(2023, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars Jedi: Survivor" },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1426210/capsule_616x353.jpg", "Впуснете се в най-лудото пътешествие в живота си.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 79.99m, new DateTime(2021, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "It Takes Two" },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1086940/capsule_616x353.jpg", "Съберете отряда си и се завърнете във Forgotten Realms.", new Guid("ffff0000-ffff-ffff-ffff-ffffffffffff"), 119.99m, new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baldur's Gate 3" },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1245620/capsule_616x353.jpg", "НОВАТА ФЕНТЪЗИ ЕКШЪН RPG ИГРА. Издигнете се и станете Elden Lord.", new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.00m, new DateTime(2022, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring" },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/814380/capsule_616x353.jpg", "Влезте в ролята на 'едноръкия вълк', опозорен и обезобразен воин.", new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.00m, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sekiro: Shadows Die Twice" },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/2208920/capsule_616x353.jpg", "Станете Ейвор, легендарен викингски рейдер.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 119.99m, new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assassin's Creed Valhalla" },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/359550/capsule_616x353.jpg", "Включете се в напрегнати, близки битки, тактика и отборна игра.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 39.99m, new DateTime(2015, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rainbow Six Siege" },
                    { new Guid("00000010-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1222670/capsule_616x353.jpg", "Създавайте и контролирайте хора в един виртуален свят без правила.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 0.00m, new DateTime(2014, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Sims 4" },
                    { new Guid("00000011-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/2195250/capsule_616x353.jpg", "Новата ера на световната игра.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 139.99m, new DateTime(2023, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "EA SPORTS FC 24" },
                    { new Guid("00000012-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1328670/capsule_616x353.jpg", "Една от най-великите научно-фантастични поредици на всички времена.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 119.99m, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mass Effect Legendary Edition" },
                    { new Guid("00000013-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/812140/capsule_616x353.jpg", "Напишете собствената си Одисея в Древна Гърция.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 119.00m, new DateTime(2018, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assassin's Creed Odyssey" },
                    { new Guid("00000014-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/2369390/capsule_616x353.jpg", "Добре дошли в Яра, тропически рай, замръзнал във времето.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 119.00m, new DateTime(2021, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Far Cry 6" },
                    { new Guid("00000015-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/2239550/capsule_616x353.jpg", "Изградете съпротива от всекиго, когото срещнете по улиците на Лондон.", new Guid("eeee0000-eeee-eeee-eeee-eeeeeeeeeeee"), 89.99m, new DateTime(2020, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Watch Dogs: Legion" },
                    { new Guid("00000016-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/550/capsule_616x353.jpg", "Кооперативен зомби сървайвъл от първо лице.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 19.50m, new DateTime(2009, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Left 4 Dead 2" },
                    { new Guid("00000017-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/440/capsule_616x353.jpg", "Една от най-популярните онлайн екшън игри на всички времена.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0.00m, new DateTime(2007, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Team Fortress 2" },
                    { new Guid("00000018-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/374320/capsule_616x353.jpg", "Огънят угасва, а светът потъва в разруха.", new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 119.00m, new DateTime(2016, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark Souls III" },
                    { new Guid("00000019-0000-0000-0000-000000000000"), "https://images.pushsquare.com/b5f36e4f16b2f/bloodborne-ps4-playstation-4-1.large.jpg", "Изправете се пред страховете си в град Ярнам.", new Guid("aaaa1111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 39.99m, new DateTime(2015, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bloodborne" },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/435150/capsule_616x353.jpg", "Съберете групата си. Овладейте дълбока тактическа бойна система.", new Guid("ffff0000-ffff-ffff-ffff-ffffffffffff"), 89.99m, new DateTime(2017, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Divinity: Original Sin 2" },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/204100/capsule_616x353.jpg", "За Макс Пейн трагедиите, които отнеха близките му, са рани, които отказват да зараснат.", new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 39.99m, new DateTime(2012, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max Payne 3" },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/12200/capsule_616x353.jpg", "Преживейте гимназията като проблемния тийнейджър Джими Хопкинс.", new Guid("aaaa0000-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 29.99m, new DateTime(2008, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bully: Scholarship Edition" },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/2138330/capsule_616x353.jpg", "Шпионски трилър експанжън за Cyberpunk 2077.", new Guid("cccc0000-cccc-cccc-cccc-cccccccccccc"), 59.99m, new DateTime(2023, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberpunk 2077: Phantom Liberty" },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/367520/capsule_616x353.jpg", "Изследвайте огромни свързани светове в тази награждавана независима игра.", new Guid("ffff0000-ffff-ffff-ffff-ffffffffffff"), 29.99m, new DateTime(2017, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hollow Knight" },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/413150/capsule_616x353.jpg", "Наследихте старата ферма на дядо си. Време е да започнете нов живот.", new Guid("dddd0000-dddd-dddd-dddd-dddddddddddd"), 27.99m, new DateTime(2016, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stardew Valley" },
                    { new Guid("00000020-0000-0000-0000-000000000000"), "https://cdn.akamai.steamstatic.com/steam/apps/1145360/capsule_616x353.jpg", "Разсечете си път от Подземния свят в този rogue-like dungeon crawler.", new Guid("bbbb0000-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 49.99m, new DateTime(2020, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hades" }
                });

            migrationBuilder.InsertData(
                table: "GameGenres",
                columns: new[] { "GameId", "GenreId", "Id" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000010-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                table: "GamePlatforms",
                columns: new[] { "GameId", "PlatformId" },
                values: new object[,]
                {
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000001-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000002-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000003-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000004-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("00000005-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000006-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000007-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000008-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000009-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000000a-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000000b-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000000c-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000000d-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000000e-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000000f-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000010-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000011-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000012-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000013-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000014-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000015-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000016-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000017-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000018-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("00000019-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000001a-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000001b-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000001c-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("0000001d-0000-0000-0000-000000000000"), new Guid("33333333-4444-5555-6666-777777777777") },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000001e-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("0000001f-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new Guid("11111111-2222-3333-4444-555555555555") },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new Guid("22222222-3333-4444-5555-666666666666") },
                    { new Guid("00000020-0000-0000-0000-000000000000"), new Guid("44444444-5555-6666-7777-888888888888") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GenreId",
                table: "GameGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_PlatformId",
                table: "GamePlatforms",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryItems_GameId",
                table: "LibraryItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryItems_UserId",
                table: "LibraryItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GameId",
                table: "OrderItems",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_GameId",
                table: "Reviews",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_GameId",
                table: "Wishlists",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "LibraryItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Developers");
        }
    }
}
