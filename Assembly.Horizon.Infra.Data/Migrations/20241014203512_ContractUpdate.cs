using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContractUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerminationClauses",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TermsAndConditions",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Horizon",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                schema: "Horizon",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                schema: "Horizon",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentFrequency",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdditionalFees",
                schema: "Horizon",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "ContractType",
                schema: "Horizon",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceDetails",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDeposit",
                schema: "Horizon",
                table: "Contracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignatureDate",
                schema: "Horizon",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Horizon",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TemplateVersion",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractType",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InsuranceDetails",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SecurityDeposit",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "SignatureDate",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TemplateVersion",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Horizon",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                schema: "Horizon",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                schema: "Horizon",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentFrequency",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<double>(
                name: "AdditionalFees",
                schema: "Horizon",
                table: "Contracts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "TerminationClauses",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TermsAndConditions",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
