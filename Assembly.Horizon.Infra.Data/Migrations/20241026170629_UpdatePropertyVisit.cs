using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePropertyVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Customer_CustomerId",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyVisits_CustomerId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "IX_PropertyVisits_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "Horizon",
                table: "PropertyVisits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Users_UserId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "UserId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Users_UserId",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyVisits_UserId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "IX_PropertyVisits_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Customer_CustomerId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "CustomerId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
