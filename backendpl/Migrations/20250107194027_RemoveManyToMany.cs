using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NovelUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NovelUser",
                columns: table => new
                {
                    FavoritedByUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NovelsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelUser", x => new { x.FavoritedByUserId, x.NovelsId });
                    table.ForeignKey(
                        name: "FK_NovelUser_Novels_NovelsId",
                        column: x => x.NovelsId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelUser_Users_FavoritedByUserId",
                        column: x => x.FavoritedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NovelUser_NovelsId",
                table: "NovelUser",
                column: "NovelsId");
        }
    }
}
