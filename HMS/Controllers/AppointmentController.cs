using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class AppointmentController : Controller
    {
        public static List<Appointment> _appointments = new();
        public IActionResult Index()
        {
            Seeds seeds = new Seeds();
            _appointments = seeds.SeedAppointment();
            return View(_appointments);
        }
    }
}
