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
        public DoctorController(IDoctorService doctorService, HmsContext hmsContext, IMasterService masterService)
        {
            _doctorService = doctorService;
            _hmsContext = hmsContext;
            _masterService = masterService;
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
