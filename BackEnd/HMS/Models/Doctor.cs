using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Doctor
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Specialization { get; set; }

    public int? Experience { get; set; }

    public string? ContactNumber { get; set; }

    public string? Email { get; set; }

    public Guid? DepartmentId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; } = new List<Appointment>();

    public virtual ICollection<Billing> Billings { get; } = new List<Billing>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();
}
