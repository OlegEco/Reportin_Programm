using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class GoodController : Controller
    {
        private readonly IGenericRepository<Good> _repositoryGood;
        public GoodController(IGenericRepository<Good> repositoryGood)
        {
            _repositoryGood = repositoryGood;
        }

        // GET: GoodController
        public async Task<IActionResult> Index()
        {
            var goods = await _repositoryGood.GetAll();
            return View(goods);
        }

        // GET: GoodController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var goods = await _repositoryGood.GetById(id.Value);
            if (goods == null)
                return NotFound();

            return View(goods);
        }

        // GET: GoodController/Create
        public IActionResult Create()
        {
            return View(new Good());
        }

        // POST: GoodController/Create
        [HttpPost]
        public async Task<IActionResult> Create(Good good)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _repositoryGood.Add(good);
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
        public async Task<IActionResult> Update(Guid? id)
        {
            if (id == null)
                return NotFound();

            var good = await _repositoryGood.GetById(id.Value);
            if (good == null)
                return NotFound();

            return View(good);
        }

        // POST: GoodController/Edit/5
        [HttpPost]
        public async Task<IActionResult> Update(Guid id, Good good)
        {
            if (id != good.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var updated = await _repositoryGood.Update(good);
                if (!updated)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(good);
        }

        // GET: GoodController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var goods = await _repositoryGood.GetById(id.Value);
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
                var good = await _repositoryGood.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
