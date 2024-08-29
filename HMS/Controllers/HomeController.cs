﻿using HMS.Abstractions;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITrackingService _trackingService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly HmsContext _context;
        

        public HomeController(ILogger<HomeController> logger, ITrackingService trackingService, IPatientService patientService, IDoctorService doctorService,HmsContext context)
        {
            _trackingService = trackingService;
            _logger = logger;
            _patientService = patientService;
            _doctorService = doctorService;
            _context = context;
        }

        public IActionResult Index()
        {
            _trackingService.IncrementVisitCount();
            var totalPatients = _context.Patients.Count();
            var totalDoctors = _context.Doctors.Count();
            
            var totalVisits = _trackingService.GetTotalVisits();
            ViewBag.TotalVisits = totalVisits;
            ViewBag.TotalPatients = totalPatients;
            ViewBag.TotalDoctors = totalDoctors;
            
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
}
