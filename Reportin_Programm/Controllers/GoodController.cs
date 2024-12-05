using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;
using System.Runtime.InteropServices;

namespace Reportin_Programm.Controllers
{
    public class GoodController : Controller
    {
        private readonly EfCoreDbContext _context;

        public GoodController(EfCoreDbContext context)
        {
            _context = context;
        }
        // GET: GoodController
        public async Task<IActionResult> Index()
        {
            var goods = await _context.Goods.ToListAsync();
            return View();
        }

        // GET: GoodController/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var goods = await _context.Goods.FirstOrDefaultAsync(gi => gi.Id == id);
            if (goods == null)
                return NotFound();

            return View(goods);
        }

        // GET: GoodController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GoodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Good good)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(good);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(good);
            }
            catch
            {
                return View();
            }
        }

        // GET: GoodController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var good = await _context.Goods.FindAsync(id);
                if (good == null)
                    return NotFound();

                return View(good);
            }
            catch //TODO Need added to Serilog all catch Exception
            {
                return View();
            }

        }

        // POST: GoodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Good good)
        {
            if (id != good.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(good);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException dbEx)
                {
                    if (!GoodExist(good.Id))
                        return NotFound();
                    else
                        throw new ArgumentException(dbEx.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: GoodController/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var goods = await _context.Goods.FirstOrDefaultAsync(gi => gi.Id == id);
            if (goods == null)
                return NotFound();

            return View(goods);
        }

        // POST: GoodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, Good good)
        {
            if (id != good.Id)
                return NotFound();
            try
            {
                var goods = await _context.Goods.FindAsync(id);
                if (goods != null)
                {
                    _context.Remove(goods);
                    await _context.SaveChangesAsync();
                }
                else
                    if (good == null)
                        return NotFound();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool GoodExist(Guid id)
        {
            return _context.Goods.Any(gi => gi.Id == id);
        }
    }
}
