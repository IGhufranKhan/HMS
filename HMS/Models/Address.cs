using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Address
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public bool? IsActive { get; set; } = true;

    public virtual ICollection<Patient> Patients { get; } = new List<Patient>();
}
