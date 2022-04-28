using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class updateUsertbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "Password", "RegisterDate" },
                values: new object[] { "802a1d4da45048cda2acc76c8f5e1ab8", "123", new DateTime(2022, 3, 11, 13, 48, 28, 467, DateTimeKind.Local).AddTicks(2275) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "c7c0543199d5487291364bbc8b73447c", new DateTime(2022, 3, 11, 9, 9, 23, 576, DateTimeKind.Local).AddTicks(3485) });
        }
    }
}
