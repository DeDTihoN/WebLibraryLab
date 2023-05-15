using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibraryApplication1.Models;

public partial class Album
{
    public int Id { get; set; }
    [Display(Name = "Назва")]
    public string? Name { get; set; }
    [Display(Name = "Опис")]
    public string? Description { get; set; }
    [Display(Name = "Релізна дата")]
    [DataType(DataType.Date)]
    public DateTime? DateOfRelease { get; set; }
    [Display(Name = "Студійна інформація")]
    public string? RecordingInfo { get; set; }
    [Display(Name = "Артист")]
    public int ArtistId { get; set; }
    [Display(Name = "Артист")]

    public virtual Artist Artist { get; set; } = null!;

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
