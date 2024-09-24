using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class OwnerProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Horizon",
                table: "Properties",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_UserId",
                schema: "Horizon",
                table: "Properties",
                newName: "IX_Properties_OwnerId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Customer_OwnerId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "Horizon",
                table: "Properties",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OwnerId",
                schema: "Horizon",
                table: "Properties",
                newName: "IX_Properties_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_UserId",
                schema: "Horizon",
                table: "Properties",
                column: "UserId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
