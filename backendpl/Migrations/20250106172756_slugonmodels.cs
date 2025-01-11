using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class slugonmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreNovel");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Novels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NovelIds",
                table: "Genres",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Genres",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NovelGenre",
                columns: table => new
                {
                    GenreId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NovelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelGenre", x => new { x.GenreId, x.NovelId });
                    table.ForeignKey(
                        name: "FK_NovelGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelGenre_Novels_NovelId",
                        column: x => x.NovelId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NovelGenre_NovelId",
                table: "NovelGenre",
                column: "NovelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NovelGenre");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Novels");

            migrationBuilder.DropColumn(
                name: "NovelIds",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "GenreNovel",
                columns: table => new
                {
                    GenresId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NovelsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreNovel", x => new { x.GenresId, x.NovelsId });
                    table.ForeignKey(
                        name: "FK_GenreNovel_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreNovel_Novels_NovelsId",
                        column: x => x.NovelsId,
                        principalTable: "Novels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreNovel_NovelsId",
                table: "GenreNovel",
                column: "NovelsId");
        }
    }
}
