using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HMS.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillingService _billingService;
        private readonly HmsContext _hmsContext;
        public BillingController(IBillingService billingService, HmsContext hmsContext)
        {
            _billingService = billingService;
            _hmsContext = hmsContext;
        }
        public IActionResult Index(string searchName)
        {

            var billings = _hmsContext.Billings.ToList();
            //if (!string.IsNullOrEmpty(searchName))
            //{
            //    billings = billings.Where(x => x.Contains(searchName)).ToList();
            //}
            return View(billings);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Billing billing)
        {
            _billingService.AddBilling(billing);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var model = new Billing();
            model = _billingService.GetBillingById(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Billing billing)
        {
            var model = _billingService.GetBillingById(billing.Id);

            _billingService.DeleteBilling(model);
            _billingService.AddBilling(billing);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var model = _billingService.GetBillingById(id);
            return View(model);
        }
        public IActionResult Delete(Guid id)
        {
            var model = _billingService.GetBillingById(id);
            _billingService.DeleteBilling(model);
            return RedirectToAction("Index");
        }

    }
}
