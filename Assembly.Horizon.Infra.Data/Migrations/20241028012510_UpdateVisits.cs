using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVisits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Realtors_RealtorId",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.RenameColumn(
                name: "RealtorId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "RealtorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyVisits_RealtorId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "IX_PropertyVisits_RealtorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Users_RealtorUserId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "RealtorUserId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyVisits_Users_RealtorUserId",
                schema: "Horizon",
                table: "PropertyVisits");

            migrationBuilder.RenameColumn(
                name: "RealtorUserId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "RealtorId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyVisits_RealtorUserId",
                schema: "Horizon",
                table: "PropertyVisits",
                newName: "IX_PropertyVisits_RealtorId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyVisits_Realtors_RealtorId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "RealtorId",
                principalSchema: "Horizon",
                principalTable: "Realtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
