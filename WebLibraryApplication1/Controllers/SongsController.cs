using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLibraryApplication1.Models;

namespace WebLibraryApplication1.Controllers
{
    public class SongsController : Controller
    {
        private readonly DblibraryContext _context;

        public SongsController(DblibraryContext context)
        {
            _context = context;
        }

        public IActionResult CreateSingle()
        {
            ViewBag.Artists = _context.Artists.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateSingle(Song song)
        {
            ModelState["Album.Artist"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                // Create album with the same name as the song, "Сінгл" description, and today's date as the release date
                var album = new Album
                {
                    Name = song.Name,
                    Description = "Сінгл",
                    ArtistId = song.Album.ArtistId,
                    RecordingInfo = song.Album.RecordingInfo,
                    DateOfRelease = DateTime.Now.Date
                };

                _context.Albums.Add(album);
                _context.SaveChanges();

                // Assign the album to the song
                song.AlbumId = album.Id;
                song.Album = null;
                _context.Songs.Add(song);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Artists = _context.Artists.ToList();
            return View(song);
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            return _context.Songs != null ?
            View(await _context.Songs.Include(s=>s.Album).ToListAsync()) :
            Problem("Entity set 'DblibraryContext.Songs'  is null.");
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,Duration,AlbumId")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumId"] = new SelectList(_context.Albums, "Id", "Id", song.AlbumId);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,Duration,AlbumId")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }
            ModelState["Album"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Album)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Songs == null)
            {
                return Problem("Entity set 'DblibraryContext.Songs'  is null.");
            }
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
          return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
