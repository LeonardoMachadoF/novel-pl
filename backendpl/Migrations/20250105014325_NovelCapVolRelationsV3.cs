using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class NovelCapVolRelationsV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Novels_NovelId",
                table: "Chapter");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreNovel_Genre_GenresId",
                table: "GenreNovel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Chapter",
                newName: "Chapters");

            migrationBuilder.RenameIndex(
                name: "IX_Chapter_NovelId",
                table: "Chapters",
                newName: "IX_Chapters_NovelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_Novels_NovelId",
                table: "Chapters",
                column: "NovelId",
                principalTable: "Novels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreNovel_Genres_GenresId",
                table: "GenreNovel",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_Novels_NovelId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreNovel_Genres_GenresId",
                table: "GenreNovel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chapters",
                table: "Chapters");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Chapters",
                newName: "Chapter");

            migrationBuilder.RenameIndex(
                name: "IX_Chapters_NovelId",
                table: "Chapter",
                newName: "IX_Chapter_NovelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chapter",
                table: "Chapter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Novels_NovelId",
                table: "Chapter",
                column: "NovelId",
                principalTable: "Novels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreNovel_Genre_GenresId",
                table: "GenreNovel",
                column: "GenresId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
