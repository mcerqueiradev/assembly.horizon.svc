using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContractInvoiceTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customer_CustomerId",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Properties_PropertyId",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Realtors_RealtorId",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customer_CustomerId",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Properties_PropertyId",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Realtors_RealtorId",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerId",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RealtorId",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsBuyer",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsTenant",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "Horizon",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TemplateVersion",
                schema: "Horizon",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "Horizon",
                newName: "Transaction",
                newSchema: "Horizon");

            migrationBuilder.RenameTable(
                name: "Contracts",
                schema: "Horizon",
                newName: "Contract",
                newSchema: "Horizon");

            migrationBuilder.RenameColumn(
                name: "RealtorId",
                schema: "Horizon",
                table: "Transaction",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "PropertyId",
                schema: "Horizon",
                table: "Transaction",
                newName: "ContractId");

            migrationBuilder.RenameColumn(
                name: "Invoice",
                schema: "Horizon",
                table: "Transaction",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PropertyId",
                schema: "Horizon",
                table: "Transaction",
                newName: "IX_Transaction_ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_RealtorId",
                schema: "Horizon",
                table: "Contract",
                newName: "IX_Contract_RealtorId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_PropertyId",
                schema: "Horizon",
                table: "Contract",
                newName: "IX_Contract_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerId",
                schema: "Horizon",
                table: "Contract",
                newName: "IX_Contract_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionHistory",
                schema: "Horizon",
                table: "Transaction",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                schema: "Horizon",
                table: "Transaction",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Horizon",
                table: "Transaction",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "Horizon",
                table: "Transaction",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DurationInMonths",
                schema: "Horizon",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                schema: "Horizon",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                schema: "Horizon",
                table: "Contract",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Contract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Horizon",
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "Horizon",
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_ContractId",
                schema: "Horizon",
                table: "Invoice",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_TransactionId",
                schema: "Horizon",
                table: "Invoice",
                column: "TransactionId",
                unique: true,
                filter: "[TransactionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                schema: "Horizon",
                table: "Contract",
                column: "CustomerId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Properties_PropertyId",
                schema: "Horizon",
                table: "Contract",
                column: "PropertyId",
                principalSchema: "Horizon",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Realtors_RealtorId",
                schema: "Horizon",
                table: "Contract",
                column: "RealtorId",
                principalSchema: "Horizon",
                principalTable: "Realtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Contract_ContractId",
                schema: "Horizon",
                table: "Transaction",
                column: "ContractId",
                principalSchema: "Horizon",
                principalTable: "Contract",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Customer_CustomerId",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Properties_PropertyId",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Realtors_RealtorId",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Contract_ContractId",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "Horizon");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Horizon",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "DurationInMonths",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "Transaction",
                schema: "Horizon",
                newName: "Transactions",
                newSchema: "Horizon");

            migrationBuilder.RenameTable(
                name: "Contract",
                schema: "Horizon",
                newName: "Contracts",
                newSchema: "Horizon");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "Horizon",
                table: "Transactions",
                newName: "Invoice");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                schema: "Horizon",
                table: "Transactions",
                newName: "RealtorId");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                schema: "Horizon",
                table: "Transactions",
                newName: "PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_ContractId",
                schema: "Horizon",
                table: "Transactions",
                newName: "IX_Transactions_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_RealtorId",
                schema: "Horizon",
                table: "Contracts",
                newName: "IX_Contracts_RealtorId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_PropertyId",
                schema: "Horizon",
                table: "Contracts",
                newName: "IX_Contracts_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_CustomerId",
                schema: "Horizon",
                table: "Contracts",
                newName: "IX_Contracts_CustomerId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionHistory",
                schema: "Horizon",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                schema: "Horizon",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Horizon",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "Horizon",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsBuyer",
                schema: "Horizon",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTenant",
                schema: "Horizon",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TransactionStatus",
                schema: "Horizon",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Value",
                schema: "Horizon",
                table: "Transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TemplateVersion",
                schema: "Horizon",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                schema: "Horizon",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                schema: "Horizon",
                table: "Contracts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                schema: "Horizon",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RealtorId",
                schema: "Horizon",
                table: "Transactions",
                column: "RealtorId");

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
                name: "FK_Contracts_Properties_PropertyId",
                schema: "Horizon",
                table: "Contracts",
                column: "PropertyId",
                principalSchema: "Horizon",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Realtors_RealtorId",
                schema: "Horizon",
                table: "Contracts",
                column: "RealtorId",
                principalSchema: "Horizon",
                principalTable: "Realtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customer_CustomerId",
                schema: "Horizon",
                table: "Transactions",
                column: "CustomerId",
                principalSchema: "Horizon",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Properties_PropertyId",
                schema: "Horizon",
                table: "Transactions",
                column: "PropertyId",
                principalSchema: "Horizon",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Realtors_RealtorId",
                schema: "Horizon",
                table: "Transactions",
                column: "RealtorId",
                principalSchema: "Horizon",
                principalTable: "Realtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
