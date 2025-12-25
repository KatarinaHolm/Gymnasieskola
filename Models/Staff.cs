using System;
using System.Collections.Generic;

namespace Gymnasieskola.Models;

public partial class Staff
{
    public int StaffId { get; set; }

    public string Profession { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string SocialSecurityNr { get; set; } = null!;

    public string HomeAddress { get; set; } = null!;

    public string PhoneNr { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? DepartmentId { get; set; }

    public virtual ICollection<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department? Department { get; set; }
}
