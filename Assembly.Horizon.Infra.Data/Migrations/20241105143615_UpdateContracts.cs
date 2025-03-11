using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProposalId",
                schema: "Horizon",
                table: "Contract",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ProposalId",
                schema: "Horizon",
                table: "Contract",
                column: "ProposalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_PropertyProposals_ProposalId",
                schema: "Horizon",
                table: "Contract",
                column: "ProposalId",
                principalSchema: "Horizon",
                principalTable: "PropertyProposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_PropertyProposals_ProposalId",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_ProposalId",
                schema: "Horizon",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "ProposalId",
                schema: "Horizon",
                table: "Contract");
        }
    }
}
