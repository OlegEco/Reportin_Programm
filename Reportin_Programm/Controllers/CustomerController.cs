using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly EfCoreDbContext _context;

        public CustomerController(EfCoreDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // GET: Customer/Create
        [HttpGet]
        public IActionResult Create()
        {
            var customer = new Customer();
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customer.Id = Guid.NewGuid();
                    _context.Add(customer);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                    return NotFound();

                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Customer customer)
        {
            if (id != customer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);

                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }

        private bool CustomerExist(Guid id) =>
             _context.Customers.Any(ci => ci.Id == id);
    }
}