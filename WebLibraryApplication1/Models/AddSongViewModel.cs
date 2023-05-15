using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebLibraryApplication1.Models
{
    public class AddSongViewModel
    {
        public Playlist Playlist;
        public int PlaylistId { get; set; }
        [Required(ErrorMessage = "Please select an album")]
        [Display(Name = "Album")]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Please select a song")]
        [Display(Name = "Song")]
        public int SongId { get; set; }

        public SelectList Albums { get; set; }

        public List<SelectListItem> SongItems { get; set; }
    }
}
