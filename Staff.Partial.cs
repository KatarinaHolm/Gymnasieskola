using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gymnasieskola.Models
{

    public partial class Staff
    {
        public Staff() { }
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
    }
}
//scaffold-DbContext “Data Source = KAPTOP; Database=school_db Integrated Security = True; Trust Server Certificate=True;” Microsoft.EntityFrameworkCore.SqlServer 
//- OutputDir Models -ContextDir Data -Context School_dbContext -Force
