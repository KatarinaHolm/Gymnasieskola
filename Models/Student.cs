using System;
using System.Collections.Generic;

namespace Gymnasieskola.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string SocialSecurityNr { get; set; } = null!;

    public string HomeAddress { get; set; } = null!;

    public string PhoneNr { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? ClassId { get; set; }

    public virtual ICollection<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();

    public virtual Class? Class { get; set; }
}
