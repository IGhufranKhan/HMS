using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Department
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? HeadOfDepartment { get; set; }

    public string? ContactNumber { get; set; }

    public virtual ICollection<Doctor> Doctors { get; } = new List<Doctor>();
}
