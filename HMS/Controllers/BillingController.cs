using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class BillingController : Controller
    {
        private readonly IBillingService _billingService;
        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }
        public IActionResult Index()
        {

            List<Billing> billings = _billingService.GetBillings();
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
