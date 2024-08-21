using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class PatientController : Controller
    {
        public static List<Patient> _patients = new();
        public IActionResult Index()
        {
            Seeds seeds = new Seeds();
            _patients = seeds.SeedPatient();
            return View(_patients);
        }
    }
}
