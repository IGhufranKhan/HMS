using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        private IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
        public IActionResult Index()
        {
            
            var doctors = _doctorService.GetDoctors();
            return View(doctors);
        }
        public IActionResult Create()
        {
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
            var model = new Doctor();
            model = _doctorService.GetDoctorById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            var model = _doctorService.GetDoctorById(doctor.Id);

            _doctorService.DeleteDoctor(model);
            _doctorService.AddDoctor(doctor);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _doctorService.GetDoctorById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var model = _doctorService.GetDoctorById(id);
            _doctorService.DeleteDoctor(model);
            return RedirectToAction("Index");
        }

    }
}
