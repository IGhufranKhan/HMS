using System;
using System.Collections.Generic;
using HMS.Models;

namespace HMS.ViewModels;

public class PatientVM
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public int? Gender { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public Guid? AddressId { get; set; }

    public Guid? DoctorId { get; set; }

    public DateTime? AdmissionDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    public virtual ICollection<Billing> Billings { get; } = new List<Billing>();

    public virtual Doctor? Doctor { get; set; }
}
