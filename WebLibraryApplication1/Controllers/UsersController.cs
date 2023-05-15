using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebLibraryApplication1.Models;

namespace WebLibraryApplication1.Controllers
{
    public class UsersController : Controller
    {
        private readonly DblibraryContext _context;

        public UsersController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Login,Password,Phone")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login && u.Id != user.Id);

                    if (existingUser != null)
                    {
                        ModelState.AddModelError("Login", "User with the same login already exists.");
                        return View(user);
                    }

                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Login,Password,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Login);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Login", "User with the same login already exists.");
                    return View(user);
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult ExportToExcel()
        {
            var users = _context.Users.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");

                worksheet.Cell(1, 1).Value = "Назва";
                worksheet.Cell(1, 2).Value = "Логін";
                worksheet.Cell(1, 3).Value = "Пароль";
                worksheet.Cell(1, 4).Value = "Телефон";

                int row = 2;
                foreach (var user in users)
                {
                    worksheet.Cell(row, 1).Value = user.Name;
                    worksheet.Cell(row, 2).Value = user.Login;
                    worksheet.Cell(row, 3).Value = user.Password;
                    worksheet.Cell(row, 4).Value = user.Phone;
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = "Users.xlsx"
                    };
                }
            }
        }

        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                ModelState.AddModelError("File", "Please select a valid Excel file.");
                return View();
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1); // Пропустити перший рядок (заголовки стовпців)

                    foreach (var row in rows)
                    {
                        var user = new User
                        {
                            Name = row.Cell(1).GetValue<string>(),
                            Login = row.Cell(2).GetValue<string>(),
                            Password = row.Cell(3).GetValue<string>(),
                            Phone = row.Cell(4).GetValue<string>()
                        };

                        _context.Users.Add(user);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
