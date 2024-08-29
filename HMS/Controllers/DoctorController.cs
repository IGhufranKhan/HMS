using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
     
        private IDoctorService _doctorService;
        private readonly HmsContext _hmsContext;
        public DoctorController(IDoctorService doctorService, HmsContext hmsContext)
        {
            _doctorService = doctorService;
            _hmsContext = hmsContext;
        }
        public IActionResult Index(string searchName)
        {
            
            var doctors = _hmsContext.Doctors.ToList();
            if(!string.IsNullOrEmpty(searchName))
            {
                doctors = doctors.Where(x => x.Name.Contains(searchName)).ToList();
            }
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
            var model = _doctorService.GetDoctorById(id);
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
