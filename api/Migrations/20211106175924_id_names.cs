using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class id_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterMovie_Characters_CharactersId",
                table: "CharacterMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterMovie_Movies_MoviesId",
                table: "CharacterMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Movies",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genres",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Characters",
                newName: "CharacterId");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "CharacterMovie",
                newName: "MoviesMovieId");

            migrationBuilder.RenameColumn(
                name: "CharactersId",
                table: "CharacterMovie",
                newName: "CharactersCharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterMovie_MoviesId",
                table: "CharacterMovie",
                newName: "IX_CharacterMovie_MoviesMovieId");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterMovie_Characters_CharactersCharacterId",
                table: "CharacterMovie",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterMovie_Movies_MoviesMovieId",
                table: "CharacterMovie",
                column: "MoviesMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterMovie_Characters_CharactersCharacterId",
                table: "CharacterMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterMovie_Movies_MoviesMovieId",
                table: "CharacterMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Movies",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genres",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "Characters",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MoviesMovieId",
                table: "CharacterMovie",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "CharactersCharacterId",
                table: "CharacterMovie",
                newName: "CharactersId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterMovie_MoviesMovieId",
                table: "CharacterMovie",
                newName: "IX_CharacterMovie_MoviesId");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterMovie_Characters_CharactersId",
                table: "CharacterMovie",
                column: "CharactersId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterMovie_Movies_MoviesId",
                table: "CharacterMovie",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Genres_GenreId",
                table: "Movies",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
