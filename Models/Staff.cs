using System;
using System.Collections.Generic;

namespace Gymnasieskola.Models;

public partial class Staff
{
    public Staff(){ }
    public Staff(string p, string fN, string lN, string sSN, string hA, string pN, string e)
    {
        Profession = p;
        FirstName = fN;
        LastName = lN;
        SocialSecurityNr = sSN;
        HomeAddress = hA;
        PhoneNr = pN;
        Email = e;        
    }

    public int StaffId { get; set; }

    public string Profession { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string SocialSecurityNr { get; set; } = null!;

    public string HomeAddress { get; set; } = null!;

    public string PhoneNr { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<AcademicRecord> AcademicRecords { get; set; } = new List<AcademicRecord>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
