using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InserUserId_Transaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionHistory",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Horizon",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                schema: "Horizon",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_UserId",
                schema: "Horizon",
                table: "Transaction",
                column: "UserId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_UserId",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserId",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "TransactionHistory",
                schema: "Horizon",
                table: "Transaction",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
