using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class updateSliderTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Slider",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "441696c84fa74343bde7a1803822d762", new DateTime(2022, 3, 15, 16, 25, 2, 535, DateTimeKind.Local).AddTicks(8997) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Slider");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "33faaf4dcec7469785f887afb5b294cf", new DateTime(2022, 3, 15, 16, 13, 52, 104, DateTimeKind.Local).AddTicks(5184) });
        }
    }
}
