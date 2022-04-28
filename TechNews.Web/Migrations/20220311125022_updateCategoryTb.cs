using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class updateCategoryTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryTitle" },
                values: new object[,]
                {
                    { 1, "اخبار ویدیو گیم" },
                    { 2, "اخبار فیلم و سریال" },
                    { 3, "اخبار تکنولوژی" },
                    { 4, "رویداد ها" },
                    { 5, "بررسی بازی" },
                    { 6, "بررسی سخت افزار" },
                    { 7, "بررسی فیلم و سریال" },
                    { 8, "مقالات" },
                    { 9, "پیشنهادی ها" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "Password", "RegisterDate" },
                values: new object[] { "74b37163bfff4e7596a0f281adad5885", "20-2C-B9-62-AC-59-07-5B-96-4B-07-15-2D-23-4B-70", new DateTime(2022, 3, 11, 16, 20, 22, 22, DateTimeKind.Local).AddTicks(8022) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "Password", "RegisterDate" },
                values: new object[] { "802a1d4da45048cda2acc76c8f5e1ab8", "123", new DateTime(2022, 3, 11, 13, 48, 28, 467, DateTimeKind.Local).AddTicks(2275) });
        }
    }
}
