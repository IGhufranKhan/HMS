using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
     
        private IDoctorService _doctorService;
        private readonly HmsContext _hmsContext;
        private readonly IMasterService _masterService;
        private IValidator<Doctor> _validator;
        public DoctorController(IDoctorService doctorService, HmsContext hmsContext, IMasterService masterService, IValidator<Doctor> validator)
        {
            _doctorService = doctorService;
            _hmsContext = hmsContext;
            _masterService = masterService;
            _validator = validator;
        }
        public IActionResult Index(string searchName)
        {
            
            var doctors = _doctorService.GetDoctors();
            if(!string.IsNullOrEmpty(searchName))
            {
                doctors = doctors.Where(x => x.Name.Contains(searchName)).ToList();
            }
            return View(doctors);
        }
        public IActionResult Create()
        {
            ViewBag.GetDepartment = _masterService.GetDepartmentName();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            ValidationResult result = _validator.Validate(doctor);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                ViewBag.GetDepartment = _masterService.GetDepartmentName();
                return View("Create", doctor);
            }
            _doctorService.AddDoctor(doctor);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var model = _doctorService.GetDoctorById(id);
            ViewBag.GetDepartment = _masterService.GetDepartmentName();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            ValidationResult result = _validator.Validate(doctor);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                ViewBag.GetDepartment = _masterService.GetDepartmentName();
                return View("Edit", doctor);
            }
            var model = _doctorService.GetDoctorById(doctor.Id);
            if(model != null)
            {
                _doctorService.UpdateDoctor(doctor);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _doctorService.GetDoctorById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            _doctorService.DeleteDoctor(id);
            return RedirectToAction("Index");
        }

    }
}
