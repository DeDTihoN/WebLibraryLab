using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using System.Security.Claims;
using System.Text;
using WebLibraryApplication1.Models;

namespace WebLibraryApplication1.Controllers
{
    [Authorize]
    public class PlaylistController : Controller
    {
        private readonly DblibraryContext _context;

        public PlaylistController(DblibraryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Playlist playlist)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            playlist.UserCreatorId = user.Id;
            playlist.DataOfCreation = DateTime.Today;

            _context.Playlists.Add(playlist);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Playlist playlist = _context.Playlists.FirstOrDefault(p => p.Id == id && p.UserCreatorId == user.Id);

            if (playlist == null)
            {
                return NotFound();
            }

            return View(playlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            Playlist playlist = _context.Playlists.FirstOrDefault(p => p.Id == id);

            if (playlist == null)
            {
                return NotFound();
            }

            _context.Playlists.Remove(playlist);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            Playlist playlist = _context.Playlists
                .Include(p => p.SongPlaylistRels)
                .ThenInclude(spr => spr.Song)
                .FirstOrDefault(p => p.Id == id);

            if (playlist == null)
            {
                return NotFound();
            }
            if (playlist.UserCreatorId != user.Id) return RedirectToAction("Index", "Login");

            return View(playlist);
        }

        [HttpGet]
        public IActionResult AddSong(int playlistId)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var playlist = _context.Playlists.Include(p => p.UserCreator).FirstOrDefault(p => p.Id == playlistId);

            if (playlist == null)
            {
                return NotFound();
            }
            if (playlist.UserCreatorId != user.Id) return RedirectToAction("Index", "Login");

        var albums = _context.Albums.ToList();

            var model = new AddSongViewModel
            {
                PlaylistId = playlistId,
                Albums = new SelectList(albums, "Id", "Name"),
                Playlist = playlist
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddSong(AddSongViewModel model)
        {
            var playlist = _context.Playlists.FirstOrDefault(p => p.Id == model.PlaylistId);

            if (playlist == null)
            {
                return NotFound();
            }

            var song = _context.Songs.FirstOrDefault(s => s.Id == model.SongId);

            if (song == null)
            {
                ModelState.AddModelError("", "Invalid song selection.");
                return View(model);
            }

            var songPlaylistRel = new SongPlaylistRel
            {
                PlaylistId = playlist.Id,
                SongId = song.Id
            };

            _context.SongPlaylistRels.Add(songPlaylistRel);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = playlist.Id });
        }


        [HttpPost]
        public IActionResult GetSongsByAlbum(int albumId)
        {
            var songs = _context.Songs.Where(s => s.AlbumId == albumId).ToList();

            var songItems = songs.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return PartialView("_SongDropdownList", songItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveSong(int playlistId, int songId)
        {
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var playlist = _context.Playlists.Include(p => p.SongPlaylistRels).FirstOrDefault(p => p.Id == playlistId);

            if (playlist == null)
            {
                return NotFound();
            }
            if (playlist.UserCreatorId != user.Id) return RedirectToAction("Index", "Login");

            var songPlaylistRel = playlist.SongPlaylistRels.FirstOrDefault(spr => spr.SongId == songId);

            if (songPlaylistRel == null)
            {
                return NotFound();
            }

            _context.SongPlaylistRels.Remove(songPlaylistRel);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = playlist.Id });
        }


        private User GetCurrentUser()
        {
            var authorized = User.FindFirst(ClaimTypes.Name);
            string login = authorized?.Value;

            if (string.IsNullOrEmpty(login))
            {
                return null;
            }

            return _context.Users.Include(s=>s.Playlists).FirstOrDefault(u => u.Login == login);
        }
    }
}
