using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IGenericRepository<Customer> _repositoryCustomer;
        private readonly IGenericRepository<Employee> _repositoryEmployee;

        public CustomerController(IGenericRepository<Customer> repositoryCustomer, IGenericRepository<Employee> repositoryEmployee)
        {
            _repositoryCustomer = repositoryCustomer;
            _repositoryEmployee = repositoryEmployee;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var customers = await _repositoryCustomer.GetAll();
            return View(customers);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var customer = await _repositoryCustomer.GetById(id.Value);
                return View(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Customer/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Customer());
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var username = HttpContext.Session.GetString("Username");
                var currentEmployee = await _repositoryEmployee.GetByPredicate(ce => ce.Username == username);

                if (currentEmployee == null)
                    return Unauthorized();

                customer.Employees = currentEmployee;

                await _repositoryCustomer.Add(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        [HttpGet]
        public async Task<IActionResult> Update(Guid? id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var customer = await _repositoryCustomer.GetById(id.Value);
                return View(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Customer customer)
        {
            if (id != customer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {

                var updated = await _repositoryCustomer.Update(customer);
                if (!updated)
                    return NotFound();

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

            try
            {
                var customer = _repositoryCustomer.GetById(id.Value);
                return View(customer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _repositoryCustomer.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}