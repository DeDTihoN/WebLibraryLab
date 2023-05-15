using Microsoft.AspNetCore.Mvc;
using WebLibraryApplication1.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebLibraryApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly DblibraryContext _context;

        public LoginController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Index(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login or password.");
                return View();
            }

            // Аутентифікуємо користувача
            var claims = new[] { new Claim(ClaimTypes.Name, user.Login) };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie");
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Playlist");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies"); // Зняття аутентифікації
            HttpContext.Response.Cookies.Delete("YourCookieName"); // Видалення кукі

            return RedirectToAction("Index");
        }
    }
}
