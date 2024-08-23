using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;



namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        public IActionResult Index()
        {
            List<Patient> patients = _patientService.GetPatients();
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
