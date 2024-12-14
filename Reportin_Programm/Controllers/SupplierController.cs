using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class SupplierController : Controller
    {
        private readonly EfCoreDbContext _context;

        public SupplierController(EfCoreDbContext context)
        {
            _context = context;
        }

        // GET: SupplierController
        public async Task<IActionResult> Index()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return View();
        }

        // GET: SupplierController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var suppliers = await _context.Suppliers.FirstOrDefaultAsync(si => si.Id == id);
            if (suppliers == null)
                return NotFound();

            return View(suppliers);
        }

        // GET: SupplierController/Create
        [HttpGet]
        public IActionResult Create()
        {
            var supplier = new Supplier();
            return View(supplier);
        }

        // POST: SupplierController/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    supplier.Id = Guid.NewGuid();
                    _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(supplier);
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var suppliers = await _context.Suppliers.FindAsync(id);
                if (suppliers == null)
                    return NotFound();

                return View(suppliers);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Supplier supplier)
        {
            if (id != supplier.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException dbEx)
                {
                    if (!SupplierExist(supplier.Id))
                        return NotFound();
                    else
                        throw new ArgumentException(dbEx.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: SupplierController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var suppliers = await _context.Suppliers.FirstOrDefaultAsync(si => si.Id == id);
            if (suppliers == null)
                return NotFound();

            return View(suppliers);
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var supplier = await _context.Suppliers.FindAsync(id);

                if (supplier != null)
                {
                    _context.Remove(id);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool SupplierExist(Guid id) =>
            _context.Suppliers.Any(si => si.Id == id);
    }
}
