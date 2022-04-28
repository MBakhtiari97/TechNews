using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class updateReviewTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DislikeCount",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "4ac089559c72413b9eac8b622699a3c8", new DateTime(2022, 3, 12, 16, 44, 28, 46, DateTimeKind.Local).AddTicks(2542) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "74b37163bfff4e7596a0f281adad5885", new DateTime(2022, 3, 11, 16, 20, 22, 22, DateTimeKind.Local).AddTicks(8022) });
        }
    }
}
