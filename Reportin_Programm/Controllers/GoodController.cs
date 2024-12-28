using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

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
            return View(goods);
        }

        // GET: GoodController/Details/5
        [HttpGet]
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
            var good = new Good();
            return View(good);
        }

        // POST: GoodController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Good good)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    good.Id = Guid.NewGuid();
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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException dbEx)
                {
                    if (!GoodExist(good.Id))
                        return NotFound();
                    else
                        throw new ArgumentException(dbEx.Message);
                }
            }
            else
                return View(good);
        }

        // GET: GoodController/Delete/5
        [HttpGet]
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
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var good = await _context.Goods.FindAsync(id);

                if (good != null)
                {
                    _context.Remove(good);
                    await _context.SaveChangesAsync();
                }

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
