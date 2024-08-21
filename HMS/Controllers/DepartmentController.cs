using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class DepartmentController : Controller
    {
        public static List<Department> _departments = new();
        public IActionResult Index()
        {
            Seeds seeds = new Seeds();
            _departments = seeds.SeedDepartment();
            return View(_departments);
            
        }
    }
}
