using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibraryApplication1.Models;

public partial class Artist
{
    public int Id { get; set; }
    [Display(Name = "Назва")]
    public string? Name { get; set; }
    [Display(Name = "Опис")]
    public string? Description { get; set; }
    [Display(Name = "Дата реєстрації")]
    [DataType(DataType.Date)]
    public DateTime? DateOfAdding { get; set; }
    [Display(Name = "Країна")]
    public string? Country { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
