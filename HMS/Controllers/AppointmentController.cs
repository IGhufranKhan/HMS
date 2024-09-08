using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly HmsContext _hmsContext;
        private readonly IMasterService _masterService;
        private IValidator<Appointment> _validator;
        public AppointmentController(IAppointmentService appointmentService, HmsContext hmsContext, IMasterService masterService, IValidator<Appointment> validator)
        {
            _appointmentService = appointmentService;
            _hmsContext = hmsContext;
            _masterService = masterService;
            _validator = validator;
        }
        public IActionResult Index(string searchName)
        {

            var model = _appointmentService.GetAppointments();
           
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.GetPatients = _masterService.GetPatientNames();
            ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            ValidationResult result = _validator.Validate(appointment);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                ViewBag.GetPatients = _masterService.GetPatientNames();
                ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
                return View("Create", appointment);
            }
            _appointmentService.AddAppointment(appointment);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            ViewBag.GetPatients = _masterService.GetPatientNames();
            ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
            var model = _appointmentService.GetAppointmentById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            ValidationResult result = _validator.Validate(appointment);
            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                ViewBag.GetPatients = _masterService.GetPatientNames();
                ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
                return View("Edit", appointment);
            }
            _appointmentService.UpdateAppointment(appointment);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _appointmentService.GetAppointmentById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            _appointmentService.DeleteAppointment(id);
            return RedirectToAction("Index");
        }

    }
}
