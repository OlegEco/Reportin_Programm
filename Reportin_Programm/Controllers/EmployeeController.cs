using Microsoft.AspNetCore.Mvc;
using Reportin_Programm.Models;

namespace Reportin_Programm.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IGenericRepository<Employee> _repositoryEmployee;

        public EmployeeController(IGenericRepository<Employee> repositoryEmployee)
        {
            _repositoryEmployee = repositoryEmployee;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _repositoryEmployee.GetAll();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _repositoryEmployee.GetById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Username,Password,Email,Phone")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _repositoryEmployee.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _repositoryEmployee.GetById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Username,Password,Email,Phone")] Employee employee)
        {
            if (id != employee.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var updated = await _repositoryEmployee.Update(employee);
                if (!updated)
                    return NotFound();

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var employee = await _repositoryEmployee.GetById(id.Value);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var employee = await _repositoryEmployee.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _repositoryEmployee.Add(employee);
                return RedirectToAction(nameof(SignIn));
            }
            return View(employee);
        }

        public IActionResult SignIn() => View();

        [HttpPost]
        public async Task<IActionResult> SignIn(string login, string password)
        {
            var employee = await _repositoryEmployee.GetByPredicate(e => e.Username == login && e.Password == password);
            if (employee == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
