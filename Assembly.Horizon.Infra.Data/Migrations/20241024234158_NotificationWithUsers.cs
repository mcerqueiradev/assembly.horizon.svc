using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class NotificationWithUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Customer_RecipientId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Realtors_SenderId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceType",
                schema: "Horizon",
                table: "Notifications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_RecipientId",
                schema: "Horizon",
                table: "Notifications",
                column: "RecipientId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_SenderId",
                schema: "Horizon",
                table: "Notifications",
                column: "SenderId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_RecipientId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_SenderId",
                schema: "Horizon",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceType",
                schema: "Horizon",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
        }
    }
}
