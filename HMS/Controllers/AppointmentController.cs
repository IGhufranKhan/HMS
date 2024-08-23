using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {

            var model = _appointmentService.GetAppointments();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            _appointmentService.AddAppointment(appointment);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var model = new Appointment();
            model = _appointmentService.GetAppointmentById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            var model = _appointmentService.GetAppointmentById(appointment.Id);

            _appointmentService.DeleteAppointment(model);
            _appointmentService.AddAppointment(appointment);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _appointmentService.GetAppointmentById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var model = _appointmentService.GetAppointmentById(id);
            _appointmentService.DeleteAppointment(model);
            return RedirectToAction("Index");
        }

    }
}
