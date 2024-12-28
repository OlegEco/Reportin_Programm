using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly EfCoreDbContext _context;

        public WarehouseController(EfCoreDbContext context)
        {
            _context = context;
        }

        // GET: WarehouseController
        public async Task<IActionResult> Index()
        {
            var warehouses = await _context.Warehouses.ToListAsync();
            return View(warehouses);
        }

        // GET: WarehouseController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var warehouses = await _context.Warehouses.FirstOrDefaultAsync(n => n.Id == id);
            if (warehouses == null)
                return NotFound();

            return View(warehouses);
        }

        // GET: WarehouseController/Create
        [HttpGet]
        public IActionResult Create()
        {
            var warehouse = new Warehouse();
            return View(warehouse);
        }

        // POST: WarehouseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    warehouse.Id = Guid.NewGuid();
                    _context.Add(warehouse);
                    await _context.SaveChangesAsync();
                    
                    return RedirectToAction(nameof(Index));
                }
                return View(warehouse);
            }
            catch
            {
                return View();
            }
        }

        // GET: WarehouseController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var warehouse = await _context.Warehouses.FindAsync(id);
                if (warehouse == null)
                    return NotFound();

                return View(warehouse);
            }
            catch
            {
                return View();
            }
        }

        // POST: WarehouseController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExist(warehouse.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: WarehouseController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var warehouse = await _context.Warehouses.FirstOrDefaultAsync(wi => wi.Id == id);

            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        // POST: WarehouseController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var warehouse = await _context.Warehouses.FindAsync(id);

                if (warehouse != null)
                {
                    _context.Warehouses.Remove(warehouse);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool WarehouseExist(Guid id) =>
             _context.Warehouses.Any(cont => cont.Id == id);

    }
}
