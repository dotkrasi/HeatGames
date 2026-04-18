using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeatGames.Data.Migrations
{
    /// <inheritdoc />
    public partial class heatmig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000000"),
                column: "CoverImageUrl",
                value: "https://image.api.playstation.com/vulcan/img/rnd/202010/2614/Sy5e8DmeKIJVjlAGraPAJYkT.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("00000019-0000-0000-0000-000000000000"),
                column: "CoverImageUrl",
                value: "https://images.pushsquare.com/b5f36e4f16b2f/bloodborne-ps4-playstation-4-1.large.jpg");
        }
    }
}
