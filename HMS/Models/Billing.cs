using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Billing
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? PatientId { get; set; }

    public Guid? DoctorId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? BillingDate { get; set; }

    public bool? IsPaid { get; set; }

    public bool? IsActive { get; set; } = true;

    public virtual Doctor? Doctor { get; set; }

    public virtual Patient? Patient { get; set; }
}
