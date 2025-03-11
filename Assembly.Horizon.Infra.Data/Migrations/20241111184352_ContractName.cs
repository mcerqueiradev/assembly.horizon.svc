using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContractName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractName",
                schema: "Horizon",
                table: "Contract",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractName",
                schema: "Horizon",
                table: "Contract");
        }
    }
}
