﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebLibraryApplication1.Models;

#nullable disable

namespace WebLibraryApplication1.Migrations
{
    [DbContext(typeof(DblibraryContext))]
    partial class DblibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebLibraryApplication1.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfRelease")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordingInfo")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Album", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("DateOfAdding")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.ArtistSongRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("ArtistSongRel", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Playlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DataOfCreation")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserCreatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserCreatorId");

                    b.ToTable("Playlist", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int");

                    b.Property<short?>("Duration")
                        .HasColumnType("smallint");

                    b.Property<string>("Genre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Song", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.SongPlaylistRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_SongPlaylistRel_1");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("SongId");

                    b.ToTable("SongPlaylistRel", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Album", b =>
                {
                    b.HasOne("WebLibraryApplication1.Models.Artist", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK_Album_Album");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.ArtistSongRel", b =>
                {
                    b.HasOne("WebLibraryApplication1.Models.Artist", "Artist")
                        .WithMany("ArtistSongRels")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK_ArtistSongRel_Artist");

                    b.HasOne("WebLibraryApplication1.Models.Song", "Song")
                        .WithMany("ArtistSongRels")
                        .HasForeignKey("SongId")
                        .IsRequired()
                        .HasConstraintName("FK_ArtistSongRel_Song");

                    b.Navigation("Artist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Playlist", b =>
                {
                    b.HasOne("WebLibraryApplication1.Models.User", "UserCreator")
                        .WithMany("Playlists")
                        .HasForeignKey("UserCreatorId")
                        .HasConstraintName("FK_Playlist_User");

                    b.Navigation("UserCreator");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Song", b =>
                {
                    b.HasOne("WebLibraryApplication1.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("FK_Song_Album");

                    b.Navigation("Album");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.SongPlaylistRel", b =>
                {
                    b.HasOne("WebLibraryApplication1.Models.Playlist", "Playlist")
                        .WithMany("SongPlaylistRels")
                        .HasForeignKey("PlaylistId")
                        .IsRequired()
                        .HasConstraintName("FK_SongPlaylistRel_Playlist");

                    b.HasOne("WebLibraryApplication1.Models.Song", "Song")
                        .WithMany("SongPlaylistRels")
                        .HasForeignKey("SongId")
                        .IsRequired()
                        .HasConstraintName("FK_SongPlaylistRel_Song");

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Artist", b =>
                {
                    b.Navigation("Albums");

                    b.Navigation("ArtistSongRels");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Playlist", b =>
                {
                    b.Navigation("SongPlaylistRels");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.Song", b =>
                {
                    b.Navigation("ArtistSongRels");

                    b.Navigation("SongPlaylistRels");
                });

            modelBuilder.Entity("WebLibraryApplication1.Models.User", b =>
                {
                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
