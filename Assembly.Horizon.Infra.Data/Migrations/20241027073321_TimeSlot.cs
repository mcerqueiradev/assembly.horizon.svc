using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class TimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.AddColumn<int>(
                name: "TimeSlot",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "VisitDate",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSlot",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
