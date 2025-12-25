using Gymnasieskola.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gymnasieskola.Models
{
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
    }
}
