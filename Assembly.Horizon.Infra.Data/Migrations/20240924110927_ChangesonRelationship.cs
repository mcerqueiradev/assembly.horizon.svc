using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangesonRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Users_UserId",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_RecipientId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Users_UserId",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Horizon",
                table: "Realtors");

            migrationBuilder.DropColumn(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Realtors");

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

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Horizon",
                table: "Contracts",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_UserId",
                schema: "Horizon",
                table: "Contracts",
                newName: "IX_Contracts_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customer_CustomerId",
                schema: "Horizon",
                table: "Contracts",
                column: "CustomerId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Customer_RecipientId",
                schema: "Horizon",
                table: "Notifications",
                column: "RecipientId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Realtors_SenderId",
                schema: "Horizon",
                table: "Notifications",
                column: "SenderId",
                principalSchema: "Horizon",
                principalTable: "Realtors",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customer_CustomerId",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Customer_RecipientId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Realtors_SenderId",
                schema: "Horizon",
                table: "Notifications");

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

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "Horizon",
                table: "Contracts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerId",
                schema: "Horizon",
                table: "Contracts",
                newName: "IX_Contracts_UserId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "Realtors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Realtors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Users_UserId",
                schema: "Horizon",
                table: "Contracts",
                column: "UserId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_RecipientId",
                schema: "Horizon",
                table: "Notifications",
                column: "RecipientId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SenderId",
                schema: "Horizon",
                table: "Notifications",
                column: "SenderId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id");

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
    }
}
