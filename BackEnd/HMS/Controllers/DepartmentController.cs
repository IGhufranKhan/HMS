using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers;

[AllowAnonymous]

public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;
    private readonly HmsContext _hmsContext;
    private IValidator<Department> _validator;
    public DepartmentController(IDepartmentService departmentService, HmsContext hmsContext, IValidator<Department> validator)
    {
        _departmentService = departmentService;
        _hmsContext = hmsContext;
        _validator = validator;
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
        ValidationResult result = _validator.Validate(department);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            
            return View("Create", department);
        }
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
        ValidationResult result = _validator.Validate(department);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            
            return View("Edit", department);
        }
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
