using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        //DataContext
        private  IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repo)
        {
            _repository = repo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve All Employees
            var employees = _repository.GetAll();
            return View(employees /*AllEmployees*/);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            // Server Side Validation
            if (!ModelState.IsValid) return View();
            _repository.Create(employee);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) => EmployeeControllerHandler(id, nameof(Details));
        //{

        //    // Retrieve Employee and send it to the view

        //    if (!id.HasValue) return BadRequest();
        //    var Employee = _repository.Get(id.Value);
        //    if (Employee is null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Employee);
        //}

        public IActionResult Edit(int? id) => EmployeeControllerHandler(id, nameof(Edit));
        //{

        //    // Retrieve Employee and send it to the view

        //    if (!id.HasValue) return BadRequest();
        //    var Employee = _repository.Get(id.Value);
        //    if (Employee is null)
        //    {
        //        return NotFound();
        //    }
        //    return View(Employee);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {

            if (id != employee.Id) return BadRequest();
            // Server Side Validation
            if (!ModelState.IsValid)
            {
                try
                {
                    _repository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log Exception
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(employee);
        }

        public IActionResult Delete(int? id) => EmployeeControllerHandler(id, nameof(Delete));
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var Employee = _repository.Get(id.Value);
        //    if(Employee is null) return NotFound(); 
        //    return View(Employee);
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _repository.Get(id.Value);
            if (employee is null) return NotFound();
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(employee);
        }


        private IActionResult EmployeeControllerHandler(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _repository.Get(id.Value);
            if (employee is null) return NotFound();
            return View(viewName, employee);
        }
    }

}
