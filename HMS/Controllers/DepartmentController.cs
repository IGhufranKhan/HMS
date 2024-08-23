using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
       
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();
            return View(departments);
        }
        public IActionResult Create(Department department)
        {
            _departmentService.AddDepartment(department);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var model = _departmentService.GetDepartmentById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            var model = _departmentService.GetDepartmentById(department.Id);
            _departmentService.DeleteDepartment(department);
            _departmentService.AddDepartment(department);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _departmentService.GetDepartmentById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var model = _departmentService.GetDepartmentById(id);
            _departmentService.DeleteDepartment(model);
            return RedirectToAction("Index");
        }
    }
}
