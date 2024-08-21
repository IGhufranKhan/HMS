using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class BillingController : Controller
    {
        public static List<Billing> _billings = new();
        public IActionResult Index()
        {
            Seeds seeds = new Seeds();
            _billings = seeds.SeedBilling();
            return View(_billings);
        }
    }
}
