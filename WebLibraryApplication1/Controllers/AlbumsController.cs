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
    public class AlbumsController : Controller
    {
        private readonly DblibraryContext _context;

        public AlbumsController(DblibraryContext context)
        {
            _context = context;
        }

        public IActionResult GetSongs(int id)
        {
            var album = _context.Albums.Include(a => a.Songs).FirstOrDefault(a => a.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        public IActionResult AddSong(int id)
        {
            var album = _context.Albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }

            var song = new Song();
            song.AlbumId = album.Id;
            
            return View(song);
        }

        [HttpPost]
        public IActionResult AddSong([Bind("Name,Genre,Duration,AlbumId")] Song song)
        {
            ModelState["Album"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                _context.Songs.Add(song);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(song);
        }

        // GET: Albums
        public async Task<IActionResult> Index()
        {
            var dblibraryContext = _context.Albums.Include(a => a.Artist);
            return View(await dblibraryContext.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public IActionResult Create()
        {
            ViewBag.ArtistList = new SelectList(_context.Artists, "Id", "Name");
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RecordingInfo,ArtistId")] Album album)
        {
            ModelState["Artist"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                album.DateOfRelease = DateTime.Today; // Встановлюємо поле DateOfRelease як сьогоднішню дату
                _context.Add(album);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
             
            ViewBag.ArtistList = new SelectList(_context.Artists, "Id", "Name", album.ArtistId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DateOfRelease,RecordingInfo,ArtistId")] Album album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }
            ModelState["Artist"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumExists(album.Id))
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
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Albums == null)
            {
                return Problem("Entity set 'DblibraryContext.Albums'  is null.");
            }
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlbumExists(int id)
        {
          return (_context.Albums?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
