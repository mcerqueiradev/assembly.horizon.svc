using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                schema: "Horizon",
                table: "Notifications",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsTransient",
                schema: "Horizon",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                schema: "Horizon",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceType",
                schema: "Horizon",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTransient",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ReferenceType",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "Horizon",
                table: "Notifications",
                newName: "Date");
        }
    }
}
