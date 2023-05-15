using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibraryApplication1.Models;

public partial class User
{
    public int Id { get; set; }
    [Display(Name = "Назва")]
    public string? Name { get; set; }
    [Display(Name = "Логін")]
    public string Login { get; set; } = null!;
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
