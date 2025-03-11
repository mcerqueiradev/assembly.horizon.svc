using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Proposal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyProposals",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposedValue = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    IntendedMoveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyProposals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyProposals_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalSchema: "Horizon",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PropertyProposals_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Horizon",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProposalNegotiations",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CounterOffer = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalNegotiations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalNegotiations_PropertyProposals_ProposalId",
                        column: x => x.ProposalId,
                        principalSchema: "Horizon",
                        principalTable: "PropertyProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalNegotiations_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "Horizon",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProposalDocuments",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProposalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NegotiationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DocumentPath = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalDocuments_PropertyProposals_ProposalId",
                        column: x => x.ProposalId,
                        principalSchema: "Horizon",
                        principalTable: "PropertyProposals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProposalDocuments_ProposalNegotiations_NegotiationId",
                        column: x => x.NegotiationId,
                        principalSchema: "Horizon",
                        principalTable: "ProposalNegotiations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyProposals_PropertyId",
                schema: "Horizon",
                table: "PropertyProposals",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyProposals_UserId",
                schema: "Horizon",
                table: "PropertyProposals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalDocuments_NegotiationId",
                schema: "Horizon",
                table: "ProposalDocuments",
                column: "NegotiationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalDocuments_ProposalId",
                schema: "Horizon",
                table: "ProposalDocuments",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalNegotiations_ProposalId",
                schema: "Horizon",
                table: "ProposalNegotiations",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalNegotiations_SenderId",
                schema: "Horizon",
                table: "ProposalNegotiations",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProposalDocuments",
                schema: "Horizon");

            migrationBuilder.DropTable(
                name: "ProposalNegotiations",
                schema: "Horizon");

            migrationBuilder.DropTable(
                name: "PropertyProposals",
                schema: "Horizon");
        }
    }
}
