using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class upatereviewtable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Reviews",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "97bc15aaa1aa49f38406234ecbb9ddb0", new DateTime(2022, 3, 13, 18, 40, 59, 263, DateTimeKind.Local).AddTicks(4759) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "29f48555b7b843ac89e9381774a30679", new DateTime(2022, 3, 13, 18, 38, 44, 281, DateTimeKind.Local).AddTicks(9292) });
        }
    }
}
