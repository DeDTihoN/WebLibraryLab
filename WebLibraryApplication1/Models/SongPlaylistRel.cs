using System;
using System.Collections.Generic;

namespace WebLibraryApplication1.Models;

public partial class SongPlaylistRel
{
    public int SongId { get; set; }

    public int PlaylistId { get; set; }

    public int Id { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
