using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using HMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace HMS.Controllers;

[AllowAnonymous]

public class BillingController : Controller
{
    private readonly IBillingService _billingService;
    private readonly HmsContext _hmsContext;
    private readonly IMasterService _masterService;
    private IValidator<Billing> _validator;
    public BillingController(IBillingService billingService, HmsContext hmsContext, IMasterService masterService, IValidator<Billing> validator)
    {
        _billingService = billingService;
        _hmsContext = hmsContext;
        _masterService = masterService;
        _validator = validator;
    }
    public IActionResult Index(string searchName)
    {

        var billings = _billingService.GetBillings();
        //if (!string.IsNullOrEmpty(searchName))
        //{
        //    billings = billings.Where(x => x.Contains(searchName)).ToList();
        //}
        return View(billings);
    }
    public IActionResult Create()
    {
        ViewBag.GetPatients = _masterService.GetPatientNames();
        ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Billing billing)
    {
        ValidationResult result = _validator.Validate(billing);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            ViewBag.GetPatients = _masterService.GetPatientNames();
            ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
            return View("Create", billing);
        }
        _billingService.AddBilling(billing);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(Guid id)
    {
        ViewBag.GetPatients = _masterService.GetPatientNames();
        ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
        var  model = _billingService.GetBillingById(id);
        return View(model);
    }
    [HttpPost]
    public IActionResult Edit(Billing billing)
    {
        ValidationResult result = _validator.Validate(billing);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            ViewBag.GetPatients = _masterService.GetPatientNames();
            ViewBag.GetDoctors = _masterService.GetDoctorDropdownList();
            return View("Edit", billing);
        }
        if (billing != null)
        {
            _billingService.UpdateBilling(billing);
        }
        return RedirectToAction("Index");
    }

    public IActionResult Details(Guid id)
    {
        var model = _billingService.GetBillingById(id);
        return View(model);
    }
    public IActionResult Delete(Guid id)
    {
        _billingService.DeleteBilling(id);
        return RedirectToAction("Index");
    }

}
