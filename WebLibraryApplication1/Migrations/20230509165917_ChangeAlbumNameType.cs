using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebLibraryApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAlbumNameType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfAdding = table.Column<DateTime>(type: "date", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfRelease = table.Column<DateTime>(type: "date", nullable: true),
                    RecordingInfo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Album_Album",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreatorId = table.Column<int>(type: "int", nullable: true),
                    DataOfCreation = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Playlist_User",
                        column: x => x.UserCreatorId,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Duration = table.Column<short>(type: "smallint", nullable: true),
                    AlbumId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Song_Album",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ArtistSongRel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSongRel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArtistSongRel_Artist",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ArtistSongRel_Song",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SongPlaylistRel",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongPlaylistRel_1", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SongPlaylistRel_Playlist",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SongPlaylistRel_Song",
                        column: x => x.SongId,
                        principalTable: "Song",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistId",
                table: "Album",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSongRel_ArtistId",
                table: "ArtistSongRel",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSongRel_SongId",
                table: "ArtistSongRel",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserCreatorId",
                table: "Playlist",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumId",
                table: "Song",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPlaylistRel_PlaylistId",
                table: "SongPlaylistRel",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_SongPlaylistRel_SongId",
                table: "SongPlaylistRel",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSongRel");

            migrationBuilder.DropTable(
                name: "SongPlaylistRel");

            migrationBuilder.DropTable(
                name: "Playlist");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
