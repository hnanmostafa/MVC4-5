using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Data;
using Demo.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PresentationLayer.Controllers
{
    public class DepartmentsController : Controller
    {
        //DataContext
        private readonly IDepartmentRepository _repository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _repository = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            // Retrieve All Departments
            var departments = _repository.GetAll();
            return View(departments /*AllDepartments*/);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department) 
        {
            // Server Side Validation
            if (!ModelState.IsValid) return View();
            _repository.Create(department);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) => DepartmentControllerHandler(id, nameof(Details));
        //{

        //    // Retrieve Department and send it to the view

        //    if (!id.HasValue) return BadRequest();
        //    var department = _repository.Get(id.Value);
        //    if (department is null)
        //    {
        //        return NotFound();
        //    }
        //    return View(department);
        //}

        public IActionResult Edit(int? id) => DepartmentControllerHandler(id, nameof(Edit));
        //{

        //    // Retrieve Department and send it to the view

        //    if (!id.HasValue) return BadRequest();
        //    var department = _repository.Get(id.Value);
        //    if (department is null)
        //    {
        //        return NotFound();
        //    }
        //    return View(department);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {

            if(id != department.Id) return BadRequest();
            // Server Side Validation
            if (!ModelState.IsValid)
            {
                try
                {
                    _repository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log Exception
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(department);
        }

        public IActionResult Delete(int? id) => DepartmentControllerHandler(id, nameof(Delete));
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var department = _repository.Get(id.Value);
        //    if(department is null) return NotFound(); 
        //    return View(department);
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);
        }


        private IActionResult DepartmentControllerHandler(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();
            var department = _repository.Get(id.Value);
            if (department is null) return NotFound();
            return View(viewName,department);
        }
    }
}
