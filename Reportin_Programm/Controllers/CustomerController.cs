using Microsoft.AspNetCore.Http;
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

        // GET: CustomerController
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View();
        }

        // GET: CustomerController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customers = await _context.Customers.FirstOrDefaultAsync(ci => ci.Id == id);
            if (customers == null)
                return NotFound();

            return View(customers);
        }

        // GET: CustomerController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
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

        // GET: CustomerController/Edit/5
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

        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Customer customer)
        {
            if (id != customer.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(customer); //TODO Change other methods to non-compliance 

            try
            {
                _context.Update(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException dbEx)
            {
                if (!CustomerExist(customer.Id))
                    return NotFound();
                else
                    throw new ArgumentException(dbEx.Message);
            }
        }


        // GET: CustomerController/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var customers = await _context.Customers.FirstOrDefaultAsync(ci => ci.Id == id);
            if (customers == null)
                return NotFound();

            return View(customers);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, Customer customer)
        {
            if (id != customer.Id)
                return NotFound();
            try
            {
                var customers = await _context.Customers.FindAsync(id);
                if (customers != null)
                {
                    _context.Remove(customer);
                    await _context.SaveChangesAsync();
                }
                else
                    if (customer == null)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool CustomerExist(Guid id) =>
             _context.Customers.Any(ci => ci.Id == id);
    }
}
