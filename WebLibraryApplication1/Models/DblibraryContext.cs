using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebLibraryApplication1.Models;

public partial class DblibraryContext : DbContext
{
    public DblibraryContext()
    {
    }

    public DblibraryContext(DbContextOptions<DblibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    public virtual DbSet<SongPlaylistRel> SongPlaylistRels { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= .\\SQLEXPRESS; Database=DBLibrary; Trusted_Connection=True;Trust Server Certificate=True; MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("Album");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateOfRelease).HasColumnType("date");

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK_Album_Album");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("Artist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Country).HasMaxLength(20);
            entity.Property(e => e.DateOfAdding).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.ToTable("Playlist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataOfCreation).HasColumnType("date");

            entity.HasOne(d => d.UserCreator).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UserCreatorId)
                .HasConstraintName("FK_Playlist_User");
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.ToTable("Song");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Album).WithMany(p => p.Songs)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK_Song_Album");
        });

        modelBuilder.Entity<SongPlaylistRel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SongPlaylistRel_1");

            entity.ToTable("SongPlaylistRel");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Playlist).WithMany(p => p.SongPlaylistRels)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("FK_SongPlaylistRel_Playlist");

            entity.HasOne(d => d.Song).WithMany(p => p.SongPlaylistRels)
                .HasForeignKey(d => d.SongId)
                .HasConstraintName("FK_SongPlaylistRel_Song");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
