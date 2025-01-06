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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("SignIn", "Employee"); 
            }

            var currentUser = _context.Employees
                .Include(e => e.Customers)
                .Include(e => e.Suppliers)
                .Include(e => e.Warehouses)
                .FirstOrDefault(e => e.Username == User.Identity.Name);

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
