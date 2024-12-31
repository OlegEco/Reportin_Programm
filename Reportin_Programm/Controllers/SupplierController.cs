using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IGenericRepository<Supplier> _repositorySupplier;
        public SupplierController(IGenericRepository<Supplier> repositorySupplier)
        {
            _repositorySupplier = repositorySupplier;
        }

        // GET: SupplierController
        public async Task<IActionResult> Index()
        {
            //var suppliers = await _context.Suppliers.ToListAsync();
            var suppliers = await _repositorySupplier.GetAll();
            return View(suppliers);
        }

        // GET: SupplierController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            //var suppliers = await _context.Suppliers.FirstOrDefaultAsync(si => si.Id == id);
            var suppliers = await _repositorySupplier.GetById(id.Value);
            if (suppliers == null)
                return NotFound();

            return View(suppliers);
        }

        // GET: SupplierController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Supplier());
        }

        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                //supplier.Id = Guid.NewGuid();
                //_context.Suppliers.Add(supplier);
                //await _context.SaveChangesAsync();
                await _repositorySupplier.Add(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: SupplierController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Update(Guid? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                //var suppliers = await _context.Suppliers.FindAsync(id);
                var suppliers = await _repositorySupplier.GetById(id.Value);
                if (suppliers == null)
                    return NotFound();

                return View(suppliers);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, Supplier supplier)
        {
            if (id != supplier.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var updated = await _repositorySupplier.Update(supplier);
                if (!updated)
                    return NotFound();

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

            try
            {
                var supplier = await _repositorySupplier.GetById(id.Value);
                return View(supplier);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var supplier = await _repositorySupplier.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
