using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLibraryApplication1.Models;

namespace WebLibraryApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DblibraryContext _context;
        public ChartController(DblibraryContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var Artists = _context.Artists.Include(l=>l.Albums).ToList(); 
            List<object> artists = new List<object>();
            artists.Add(new[] { "Виконавець" , "Кількість альбомів"});
            foreach(var c in Artists)
            {
                artists.Add(new object[] {c.Name,c.Albums.Count()});
            }
            return new JsonResult(artists);
        }
        [HttpGet("BarChartData")]
        public JsonResult BarChartData()
        {
            var albums = _context.Albums.Include(a => a.Songs).ToList();
            List<object> data = new List<object>();
            data.Add(new[] { "Альбом", "Кількість пісень" });
            foreach (var album in albums)
            {
                data.Add(new object[] { album.Name, album.Songs.Count });
            }
            return new JsonResult(data);
        }
    }
}
