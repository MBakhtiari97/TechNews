using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TechNews.Web.Migrations
{
    public partial class insertSliderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slider",
                columns: table => new
                {
                    SlideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlideName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SlideAlt = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.SlideId);
                    table.ForeignKey(
                        name: "FK_Slider_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "33faaf4dcec7469785f887afb5b294cf", new DateTime(2022, 3, 15, 16, 13, 52, 104, DateTimeKind.Local).AddTicks(5184) });

            migrationBuilder.CreateIndex(
                name: "IX_Slider_ItemId",
                table: "Slider",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slider");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "IdentifierCode", "RegisterDate" },
                values: new object[] { "3dabc6fa632c4deb8de026f4ccd122d0", new DateTime(2022, 3, 15, 7, 31, 58, 276, DateTimeKind.Local).AddTicks(9350) });
        }
    }
}
