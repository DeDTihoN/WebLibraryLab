using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibraryApplication1.Models;

public partial class Song
{
    public int Id { get; set; }
    [Display(Name = "Назва")]
    public string? Name { get; set; }
    [Display(Name = "Жанр")]
    public string? Genre { get; set; }
    [Display(Name = "Тривалість")]
    public short? Duration { get; set; }
    [Display(Name = "Альбом")]
    public int AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual ICollection<SongPlaylistRel> SongPlaylistRels { get; set; } = new List<SongPlaylistRel>();
}
