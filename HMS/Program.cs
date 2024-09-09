using FluentValidation;
using HMS.Abstractions;
using HMS.Models;
using HMS.Services;
using HMS.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HMS");
builder.Services.AddDbContext<HmsContext>(options
          => options.UseSqlServer(connectionString));
// Add dependency injections
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IContextService, ContextService>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IMasterService, MasterService>();
//Validators
builder.Services.AddScoped<IValidator<Patient>, PatientValidator>();
builder.Services.AddScoped<IValidator<Address>, AddressValidator>();
builder.Services.AddScoped<IValidator<Billing>, BillingValidator>();
builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();
builder.Services.AddScoped<IValidator<Doctor>, DoctorValidator>();
builder.Services.AddScoped<IValidator<Appointment>, AppointmentValidator>();
// Add services to the container.
builder.Services.AddControllersWithViews();

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option => {
                option.LoginPath = "/Authentication/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
            });
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
