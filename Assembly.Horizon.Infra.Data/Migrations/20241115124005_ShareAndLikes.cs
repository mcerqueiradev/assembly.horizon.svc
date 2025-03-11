using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assembly.Horizon.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShareAndLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPosts_Users_UserId",
                schema: "Horizon",
                table: "UserPosts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "UserPosts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Horizon",
                table: "UserPosts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                schema: "Horizon",
                table: "UserPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SharesCount",
                schema: "Horizon",
                table: "UserPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserPostLikes",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPostLikes_UserPosts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Horizon",
                        principalTable: "UserPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPostLikes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Horizon",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPostsShare",
                schema: "Horizon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPostsShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPostsShare_UserPosts_PostId",
                        column: x => x.PostId,
                        principalSchema: "Horizon",
                        principalTable: "UserPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPostsShare_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Horizon",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPostLikes_PostId_UserId",
                schema: "Horizon",
                table: "UserPostLikes",
                columns: new[] { "PostId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPostLikes_UserId",
                schema: "Horizon",
                table: "UserPostLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPostsShare_PostId_UserId",
                schema: "Horizon",
                table: "UserPostsShare",
                columns: new[] { "PostId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPostsShare_UserId",
                schema: "Horizon",
                table: "UserPostsShare",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_Users_UserId",
                schema: "Horizon",
                table: "UserPosts",
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
                name: "FK_UserPosts_Users_UserId",
                schema: "Horizon",
                table: "UserPosts");

            migrationBuilder.DropTable(
                name: "UserPostLikes",
                schema: "Horizon");

            migrationBuilder.DropTable(
                name: "UserPostsShare",
                schema: "Horizon");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                schema: "Horizon",
                table: "UserPosts");

            migrationBuilder.DropColumn(
                name: "SharesCount",
                schema: "Horizon",
                table: "UserPosts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "Horizon",
                table: "UserPosts",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "Horizon",
                table: "UserPosts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPosts_Users_UserId",
                schema: "Horizon",
                table: "UserPosts",
                column: "UserId",
                principalSchema: "Horizon",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
