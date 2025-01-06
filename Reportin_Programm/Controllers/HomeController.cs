using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class HomeController : Controller
    {
        private readonly EfCoreDbContext _context;

        public HomeController(EfCoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("SignIn", "Employee");
            }

            var currentUser = _context.Employees
                .Include(e => e.Customers)
                .FirstOrDefault(e => e.Username == username);

            //var currentUser = _context.Employees.FirstOrDefault(e => e.Username == username);

            // Если текущий пользователь не найден, возвращаем ошибку 404
            if (currentUser == null)
            {
                return NotFound();
            }

            return View(currentUser);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
