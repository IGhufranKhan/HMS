using System;
using System.Collections.Generic;

namespace HMS.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public string? ContactNo { get; set; }
}
