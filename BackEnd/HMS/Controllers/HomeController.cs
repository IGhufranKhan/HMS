using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HMS.Controllers;

[AllowAnonymous]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITrackingService _trackingService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IDepartmentService _departmentService;
    //private readonly HmsContext _context;


    public HomeController(ILogger<HomeController> logger, ITrackingService trackingService, IPatientService patientService, IDoctorService doctorService, IDepartmentService departmentService)
    {
        _trackingService = trackingService;
        _logger = logger;
        _patientService = patientService;
        _doctorService = doctorService;
        _departmentService = departmentService;
    }

    public IActionResult Index()
    {
        _trackingService.IncrementVisitCount();
        var totalPatients = _patientService.GetPatients().Result.Count();
        var totalDoctors = _doctorService.GetDoctors().Count();
        var totalDepartments = _departmentService.GetDepartments().Count();
        
        var totalVisits = _trackingService.GetTotalVisits();
        ViewBag.TotalVisits = totalVisits;
        ViewBag.TotalPatients = totalPatients;
        ViewBag.TotalDoctors = totalDoctors;
        ViewBag.TotalDepartments = totalDepartments;
        
        return View();
    }
    public IActionResult AboutMe()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
