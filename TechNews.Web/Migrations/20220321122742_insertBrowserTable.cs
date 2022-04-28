using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class insertBrowserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Browsers",
                columns: table => new
                {
                    BrowserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrowserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UsedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Browsers", x => x.BrowserId);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "3e1299a7fd884af8a3e1c4d29338023a", new DateTime(2022, 3, 21, 15, 57, 41, 781, DateTimeKind.Local).AddTicks(3244) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Browsers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "8f8033c6ff3d48b7a607c7924c7a1f98", new DateTime(2022, 3, 21, 9, 25, 48, 26, DateTimeKind.Local).AddTicks(8853) });
        }
    }
}
