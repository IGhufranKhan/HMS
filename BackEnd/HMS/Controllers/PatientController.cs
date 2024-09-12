using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace HMS.Controllers;
[AllowAnonymous]
public class PatientController : Controller
{
    private readonly IPatientService _patientService;
    private readonly HmsContext _hmsContext;
    private readonly IMasterService _masterService;
    private IValidator<Patient> _validator;

    public PatientController(IPatientService patientService, HmsContext hmsContext, IMasterService masterService, IValidator<Patient> validator)
    {
        _patientService = patientService;
        _hmsContext = hmsContext;
        _masterService = masterService;
        _validator = validator;
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
        
        ViewBag.Doctors = _masterService.GetDoctorDropdownList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Patient patient, IFormFile ProfilePictureId)
    {
        ValidationResult result = _validator.Validate(patient);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            ViewBag.Doctors = _masterService.GetDoctorDropdownList();
            return View("Create", patient);
        }
        patient.AddressId = patient.Address?.Id;
        if (ProfilePictureId != null)
        {
            var pictureName = "pp-" + Guid.NewGuid() + Path.GetExtension(ProfilePictureId.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", pictureName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 ProfilePictureId.CopyToAsync(stream);
            }
            patient.ProfilePictureId = pictureName;
        }

        _patientService.AddPatient(patient);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        var model = _patientService.GetPatientById(id);
       // ViewBag.AddressId = new SelectList(_hmsContext.Addresses, "Id", "Address");
        ViewBag.DoctorId = _masterService.GetDoctorDropdownList();
        return View(model);
    }
    [HttpPost]
    public IActionResult Edit(Patient patient, IFormFile ProfilePictureId)
    {
        ValidationResult result = _validator.Validate(patient);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            ViewBag.DoctorId = _masterService.GetDoctorDropdownList();
            return View("Edit", patient);
        }
        if (patient != null)
        {
            if (ProfilePictureId != null)
            {
                // Remove old picture if needed
                if (!string.IsNullOrEmpty(patient.ProfilePictureId))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", patient.ProfilePictureId);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                var pictureName = "pp-" + Guid.NewGuid() + Path.GetExtension(ProfilePictureId.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", pictureName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ProfilePictureId.CopyToAsync(stream);
                }
                patient.ProfilePictureId = pictureName;
            }

            _patientService.UpdatePatient(patient);
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
        _patientService.DeletePatient(id);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult RemovePicture(Guid id)
    {
        var patient =  _patientService.GetPatientById(id);
        if (patient == null) return NotFound();

        if (!string.IsNullOrEmpty(patient.ProfilePictureId))
        {
            // Get the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", patient.ProfilePictureId);

            // Delete the file from the file system
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove the picture reference from the database
            patient.ProfilePictureId = null;
            _patientService.UpdatePatient(patient);
            
        }
        return RedirectToAction("Edit", new { id = id });
    }

}
