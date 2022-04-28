using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class updateReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SlideBanner",
                table: "Slider",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "UserIpAddress",
                table: "Reviews",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "0a0341b3aa0a477e9cb49a07c58167ac", new DateTime(2022, 3, 19, 16, 1, 24, 87, DateTimeKind.Local).AddTicks(6141) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SlideBanner",
                table: "Slider",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserIpAddress",
                table: "Reviews",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "99536b0822bf479397dc84b469402b82", new DateTime(2022, 3, 18, 15, 55, 10, 662, DateTimeKind.Local).AddTicks(6829) });
        }
    }
}
