using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitKidCateringApp.Migrations
{
    public partial class _003_change_offer_to_cycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateUtc",
                table: "Offers");

            migrationBuilder.AddColumn<short>(
                name: "DayOfWeek",
                table: "Offers",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Offers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUtc",
                table: "Offers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
