using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class updateslidetb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slider_Items_ItemId",
                table: "Slider");

            migrationBuilder.DropIndex(
                name: "IX_Slider_ItemId",
                table: "Slider");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Slider");

            migrationBuilder.AddColumn<string>(
                name: "ItemRefersTo",
                table: "Slider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SlideSubmitDate",
                table: "Slider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "99536b0822bf479397dc84b469402b82", new DateTime(2022, 3, 18, 15, 55, 10, 662, DateTimeKind.Local).AddTicks(6829) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemRefersTo",
                table: "Slider");

            migrationBuilder.DropColumn(
                name: "SlideSubmitDate",
                table: "Slider");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Slider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "963eef2546134792aafadd5afffae2de", new DateTime(2022, 3, 18, 15, 43, 13, 863, DateTimeKind.Local).AddTicks(9440) });

            migrationBuilder.CreateIndex(
                name: "IX_Slider_ItemId",
                table: "Slider",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slider_Items_ItemId",
                table: "Slider",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
