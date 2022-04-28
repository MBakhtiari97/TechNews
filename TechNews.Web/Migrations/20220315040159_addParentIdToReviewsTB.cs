using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class addParentIdToReviewsTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Newsletter",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "3dabc6fa632c4deb8de026f4ccd122d0", new DateTime(2022, 3, 15, 7, 31, 58, 276, DateTimeKind.Local).AddTicks(9350) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Newsletter",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "7782b58560f5433b9b4a744a4c98769f", new DateTime(2022, 3, 14, 18, 33, 9, 145, DateTimeKind.Local).AddTicks(3420) });
        }
    }
}
