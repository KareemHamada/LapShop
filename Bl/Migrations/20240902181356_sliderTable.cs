using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.Migrations
{
    public partial class sliderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TbSlider",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TbSlider",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "TbSlider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TbSlider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TbSlider",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TbSliderSliderId",
                table: "TbItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbItems_TbSliderSliderId",
                table: "TbItems",
                column: "TbSliderSliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbItems_TbSlider_TbSliderSliderId",
                table: "TbItems",
                column: "TbSliderSliderId",
                principalTable: "TbSlider",
                principalColumn: "SliderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbItems_TbSlider_TbSliderSliderId",
                table: "TbItems");

            migrationBuilder.DropIndex(
                name: "IX_TbItems_TbSliderSliderId",
                table: "TbItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TbSlider");

            migrationBuilder.DropColumn(
                name: "TbSliderSliderId",
                table: "TbItems");
        }
    }
}
