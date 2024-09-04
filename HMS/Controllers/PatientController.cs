using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;



namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly HmsContext _hmsContext;
        private readonly IMasterService _masterService;
        public PatientController(IPatientService patientService, HmsContext hmsContext, IMasterService masterService)
        {
            _patientService = patientService;
            _hmsContext = hmsContext;
            _masterService = masterService;
        }
        public async Task <IActionResult> Index(string searchName)
        {
            var patients = new List<Patient>();
            ViewBag.SearchName = searchName;
            if (!string.IsNullOrEmpty(searchName))
            {
                patients = await _patientService.GetPatients(searchName);
                return View(patients);
            }
            else
            {
                patients = await _patientService.GetPatients();
                return View(patients);
            }
          
        }
        public IActionResult Create()
        {
            //var model = Seeds.SeedDoctor();
            //var model1 = model.Select(x => new { x.Id, x.Name }).ToList();
            ViewBag.Doctors = _masterService.GetDoctorDropdownList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            patient.AddressId = patient.Address?.Id;
            _patientService.AddPatient(patient);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var model = _patientService.GetPatientById(id);
            ViewBag.AddressId = new SelectList(_hmsContext.Addresses, "Id", "Address");
            ViewBag.DoctorId = new SelectList(_hmsContext.Doctors, "Id", "Name");
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            var model = _patientService.GetPatientById(patient.Id);
            if (model != null)
            {
                model.Name = patient.Name;
                model.Age = patient.Age;
                model.Gender = patient.Gender;
                model.ContactNumber = patient.ContactNumber;
                model.Email = patient.Email;
                model.AddressId = patient.AddressId;
                model.DoctorId = patient.DoctorId;
                model.AdmissionDate = patient.AdmissionDate;
                model.DischargeDate = patient.DischargeDate;
                _patientService.UpdatePatient(model);
            }

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
            if (model != null)
            {
                model.IsActive = false;
            }
            _patientService.UpdatePatient(model);
            return RedirectToAction("Index");
        }
    }
}
