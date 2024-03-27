using System;
using System.Collections.Generic;

namespace Lesson09_Session.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool? Isactive { get; set; }

    public bool? Isdelete { get; set; }
}
