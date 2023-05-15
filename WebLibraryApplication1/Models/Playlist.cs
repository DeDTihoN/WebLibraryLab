using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibraryApplication1.Models;

public partial class Playlist
{
    public int Id { get; set; }
    [Display(Name = "Назва")]
    public string? Name { get; set; }

    public int UserCreatorId { get; set; }
    [Display(Name = "Дата створення")]
    [DataType(DataType.Date)]
    public DateTime? DataOfCreation { get; set; }

    public virtual ICollection<SongPlaylistRel> SongPlaylistRels { get; set; } = new List<SongPlaylistRel>();
    [Display(Name = "Користувач")]
    public virtual User UserCreator { get; set; } = null!;
}
