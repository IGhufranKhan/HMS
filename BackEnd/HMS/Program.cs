using FluentValidation;
using HMS.Abstractions;
using HMS.Configuration;
using HMS.Models;
using HMS.Services;
using HMS.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("HMS");
builder.Services.AddDbContext<HmsContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddHttpContextAccessor();

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
builder.Services.AddScoped<IContextService, ContextService>(); 
builder.Services.AddScoped<IUploadPictureService, UploadPictureService>(); 


// Validators
builder.Services.AddScoped<IValidator<Patient>, PatientValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddScoped<IValidator<Billing>, BillingValidator>();
builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();
builder.Services.AddScoped<IValidator<Doctor>, DoctorValidator>();
builder.Services.AddScoped<IValidator<Appointment>, AppointmentValidator>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Authentication/Login"; // Your login path
        options.LogoutPath = "/Authentication/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Session expires in 60 minutes
        options.SlidingExpiration = true; // This extends the cookie expiration if user is active
    });
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax; // or None, if using external login providers
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
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
