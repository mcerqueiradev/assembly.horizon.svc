using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class newchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Horizon",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "Realtors",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Realtors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Horizon",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "Accounts",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contracts",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealtorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdditionalFees = table.Column<double>(type: "float", nullable: false),
                    PaymentFrequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RenewalOption = table.Column<bool>(type: "bit", nullable: false),
                    TerminationClauses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastActiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "Horizon",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Realtors_RealtorId",
                        column: x => x.RealtorId,
                        principalSchema: "Horizon",
                        principalTable: "Realtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Horizon",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyVisits",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RealtorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyVisits_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "Horizon",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyVisits_Realtors_RealtorId",
                        column: x => x.RealtorId,
                        principalSchema: "Horizon",
                        principalTable: "Realtors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyVisits_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Horizon",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_UserId",
                schema: "Horizon",
                table: "Properties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PropertyId",
                schema: "Horizon",
                table: "Contracts",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_RealtorId",
                schema: "Horizon",
                table: "Contracts",
                column: "RealtorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_UserId",
                schema: "Horizon",
                table: "Contracts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVisits_PropertyId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVisits_RealtorId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "RealtorId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyVisits_UserId",
                schema: "Horizon",
                table: "PropertyVisits",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_UserId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Contracts",
                schema: "Horizon");

            migrationBuilder.DropTable(
                name: "PropertyVisits",
                schema: "Horizon");

            migrationBuilder.DropIndex(
                name: "IX_Properties_UserId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Horizon",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Horizon",
                table: "Realtors");

            migrationBuilder.DropColumn(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Realtors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Horizon",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Horizon",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LastActiveDate",
                schema: "Horizon",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                schema: "Horizon",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
