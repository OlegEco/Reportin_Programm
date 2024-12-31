using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IGenericRepository<Warehouse> _repositoryWarehouse;

        public WarehouseController(IGenericRepository<Warehouse> repositoryWarehouse)
        {
            _repositoryWarehouse = repositoryWarehouse;
        }

        // GET: WarehouseController
        public async Task<IActionResult> Index()
        {
            var warehouses = await _repositoryWarehouse.GetAll();/*_context.Warehouses.ToListAsync();*/
            return View(warehouses);
        }

        // GET: WarehouseController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var warehouses = await _repositoryWarehouse.GetById(id.Value);/*_context.Warehouses.FirstOrDefaultAsync(n => n.Id == id);*/
            if (warehouses == null)
                return NotFound();

            return View(warehouses);
        }

        // GET: WarehouseController/Create
        [HttpGet]
        public IActionResult Create()
        {
            //var warehouse = new Warehouse();
            return View(new Warehouse());
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
                    //warehouse.Id = Guid.NewGuid();
                    //_context.Warehouses.Add(warehouse);
                    //await _context.SaveChangesAsync();
                    await _repositoryWarehouse.Add(warehouse);
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
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null)
                return NotFound();

            //var warehouse = await _context.Warehouses.FindAsync(id);
            var warehouse = await _repositoryWarehouse.GetById(id.Value);
            if (warehouse == null)
                return NotFound();

            return View(warehouse);
        }

        // POST: WarehouseController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
                return NotFound();

            if (ModelState.IsValid)
            {

                //_context.Update(warehouse);
                //await _context.SaveChangesAsync();
                var updated = await _repositoryWarehouse.Update(warehouse);
                if (!updated)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: WarehouseController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var warehouse = await _repositoryWarehouse.GetById(id.Value); /*_context.Warehouses.FirstOrDefaultAsync(wi => wi.Id == id);*/
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
                //var warehouse = await _context.Warehouses.FindAsync(id);

                //if (warehouse != null)
                //{
                //    _context.Warehouses.Remove(warehouse);
                //    await _context.SaveChangesAsync();
                //}
                //return RedirectToAction(nameof(Index));

                var warehouse = await _repositoryWarehouse.Delete(id);
                return RedirectToAction(nameof(Index));
            }

            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
