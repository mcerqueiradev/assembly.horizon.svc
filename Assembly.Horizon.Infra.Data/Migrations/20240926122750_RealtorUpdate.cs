using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RealtorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Customer_OwnerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_OwnerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "Horizon",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CustomerId",
                schema: "Horizon",
                table: "Properties",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Customer_CustomerId",
                schema: "Horizon",
                table: "Properties",
                column: "CustomerId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Customer_CustomerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CustomerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "Horizon",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                schema: "Horizon",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Customer_OwnerId",
                schema: "Horizon",
                table: "Properties",
                column: "OwnerId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
