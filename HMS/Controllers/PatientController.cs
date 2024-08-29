using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;



namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly HmsContext _hmsContext;
        public PatientController(IPatientService patientService, HmsContext hmsContext)
        {
            _patientService = patientService;
            _hmsContext = hmsContext;
        }
        public IActionResult Index(string searchName)
        {
            var patients = _hmsContext.Patients.ToList();
            //List<PatientVM> patientss = _hmsContext.Patients.ToList();
            //patientss = patients;
            ViewBag.SearchName = searchName;
            if (!string.IsNullOrEmpty(searchName))
            {
                patients = patients.Where(x => x.Name.Contains(searchName)).ToList();
            }
            return View(patients);
        }
        public IActionResult Create()
        {
            //var model = Seeds.SeedDoctor();
            //var model1 = model.Select(x => new { x.Id, x.Name }).ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            _patientService.AddPatient(patient);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var model = _patientService.GetPatientById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            var model = _patientService.GetPatientById(patient.Id);
            _patientService.DeletePatient(patient);
            _patientService.AddPatient(patient);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _patientService.GetPatientById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var model = _patientService.GetPatientById(id);
            _patientService.DeletePatient(model);
            return RedirectToAction("Index");
        }
    }
}
