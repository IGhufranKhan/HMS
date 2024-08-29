using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly HmsContext _hmsContext;
        public DepartmentController(IDepartmentService departmentService, HmsContext hmsContext)
        {
            _departmentService = departmentService;
            _hmsContext = hmsContext;
        }

        public IActionResult Index(string searchName)
        {
            var departments = _hmsContext.Departments.ToList();
            if (!string.IsNullOrEmpty(searchName))
            {
                departments = departments.Where(x => x.Name.Contains(searchName)).ToList();
            }
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
