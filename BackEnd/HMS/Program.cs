using FluentValidation;
using HMS.Abstractions;
using HMS.Configuration;
using HMS.Models;
using HMS.Services;
using HMS.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("HMS");
builder.Services.AddDbContext<HmsContext>(options =>
    options.UseSqlServer(connectionString));

// Configuration for email service
var configuration = builder.Configuration;
builder.Services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
builder.Services.AddScoped<IEmailService, EmailService>();

// Add dependency injections for services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IContextService, ContextService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

// Add patient and doctor-related services
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IAuthtenticationService, AuthtenticationService>();  // Fix: Typo in "Authentication"

// Validators
builder.Services.AddScoped<IValidator<Patient>, PatientValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddScoped<IValidator<Billing>, BillingValidator>();
builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();
builder.Services.AddScoped<IValidator<Doctor>, DoctorValidator>();
builder.Services.AddScoped<IValidator<Appointment>, AppointmentValidator>();

// Add services to the container
builder.Services.AddControllersWithViews();

// HttpContext accessor (needed for authentication)
builder.Services.AddHttpContextAccessor();

// Authentication setup using cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //options.LoginPath = "/Authentication/Login"; // Path to login page
        //options.LogoutPath = "/Authentication/Logout"; // Path to logout action
        //options.Cookie.HttpOnly = true; // Ensures cookies are only accessible via HTTP requests
        //options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Adjust as needed for HTTPS
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Adjust expiration time
        //options.SlidingExpiration = true; // Renew the cookie before expiration
        //options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Use this for development without HTTPS
        options.LoginPath = "/Authentication/Login"; // Path to login page
        options.LogoutPath = "/Authentication/Logout"; // Path to logout action
        options.Cookie.HttpOnly = true; // Ensure cookies are only accessible via HTTP
        options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Use None for HTTP during development
        options.ExpireTimeSpan = TimeSpan.FromHours(1); // Set a longer expiration time
        options.SlidingExpiration = true; // Renew cookie before expiration
        options.Cookie.SameSite = SameSiteMode.Lax; // Adjust SameSite as needed
        options.Cookie.Name = "MyAuthCookie"; // Custom cookie name


    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Ensure authentication comes before authorization
app.UseAuthentication();
app.UseAuthorization();

// Define routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Login}/{id?}");

app.Run();
