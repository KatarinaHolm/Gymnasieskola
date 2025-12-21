using System;
using System.Collections.Generic;

namespace Gymnasieskola.Models;

public partial class Student
{
    public Student() { }
    public Student(string fN, string lN, string sSN, string hA, string pN, string e, int? cI)
    {
        FirstName = fN;
        LastName = lN;
        SocialSecurityNr = sSN;
        HomeAddress = hA;
        PhoneNr = pN;
        Email = e;
        ClassId = cI;
    }
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
