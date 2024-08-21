using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DoctorController : Controller
    {
        public static List<Doctor> _doctors = new();
        public IActionResult Index()
        {
            Seeds seeds = new Seeds();
            _doctors = seeds.SeedDoctor();
            return View(_doctors);
        }
    }
}
